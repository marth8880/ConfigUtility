![App banner](Images/app_banner_horizontal.jpg)

# Mod Configurator

This is a tool that enables Star Wars Battlefront II mod authors to allow players to configure their mods.

When implemented, Mod Configurator works on an individual mod basis - meaning that it only affects one mod. The same instance of the tool cannot be used to configure multiple mods within the same program. For example, if I have Mod A and Mod B and want players to be able to configure both, I must implement Mod Configurator separately in Mod A _and_ in Mod B. I cannot implement it in one to configure both. (Okay, technically speaking I could have Mod B read and load the compiled settings script from Mod A, but you get the point.)

Mod authors can set up tabs and dropdown settings in an external application through a simple JSON file, cleverly called `config.json`. When a player changes a setting, the settings are saved and munged into a single `.SCRIPT` file as a global Lua table. The settings are simple key(string)-value(number) pairs in the table.

## Use Cases

You might be thinking, what could you possibly need this for? Well, imagine you're working on an era mod with many different factions. You currently have it set up that the faction combination is randomly selected at the start of each match, but your players might want to be able to specify which faction combination they want to fight in. Well that's the exact use case for this tool!

Only have two factions in your mod? Maybe some of your maps have random weather variations. It would be nice to give players the option to specify what weather variation they want to play on for that map - maybe they absolutely _despise_ your night variation but love the snowy variation.

At the end of the day, this tool really just compiles user-set values into a Lua table with key-value integer pairs, so if you can think of something else to do with that, it can probably be done.

## Setting Up a Mod Configuration

The tabs and settings are configured by the mod author through the bundled `config.json` file. Please see [HERE](https://cheatography.com/gaston/cheat-sheets/json/ "JSON cheat sheet") for more about JSON file syntax (but really it's pretty simple).

Inside the initial pair of curly brackets, here are the following fields:

| Field | Type | Required | Comment |  
|---|---|---|---|  
| fileVersion | Integer | Yes |  | 
| mungedScriptFileName | String | Yes | File name of the `.SCRIPT` file that gets munged |  
| userConfigLuaTableName | String | Yes | Name of the Lua table that gets compiled into the `.SCRIPT` file |  
| configTabs | Array (objects) | Yes | Collection of tab descriptors |  

An object in the `configTabs` array can have the following fields:

| Field | Type | Required | Comment |  
|---|---|---|---|  
| name | String | Yes | Text name of the tab as displayed in the tab bar |  
| description | String | No | Text description that gets displayed at the top of the tab page |  
| footnote | String | No | Text description that gets displayed at the bottom of the tab page |  
| flags | Array (objects) | Yes | Collection of setting descriptors |  

An object in the `flags` array can have the following fields:

| Field | Type | Required | Comment |  
|---|---|---|---|  
| path | String | Yes | Unique name (a-Z characters only) of the Lua table element that this setting's value gets stored in |  
| name | String | Yes | Text name of the setting that gets displayed next to the setting's dropdown box |  
| toolTipCaption | String | No | Text description of the setting that gets displayed in a tooltip bubble when hovering over the setting's dropdown or name label |  
| values | Array (strings) | Yes | Text names of the setting's possible values, which are shown in the dropdown menu box |  
| defaultValue | Integer | No | Default value for the setting, which corresponds with the ordering of the `values` array elements - for example, in an array of `["Apple, "Banana", "Orange"]`, a `defaultValue` of 2 would make the default setting `Orange`. Uses value of 0 if unspecified |  

### Remarks

* The spelling and capitalization of field names must be exact. Any variation will result in a syntax error. For example, `configFlags` must be called `configFlags`, not `ConfigFlags`, `configflags`, or anything else.
* `config.json` must be located in the same folder as `ConfigUtility.exe`. Without it the application cannot function.
* It is recommended to use a unique Lua table name for mods that would load the munged script into a menu (such as in the addme), since more than one script of the same name cannot be loaded into the game at once. The Lua table name can be set via the `userConfigLuaTableName` field in the JSON file. A way to reasonably guarantee a unique table name would be to simply append the mod's ID to the name, e.g. `gMEUModConfig`.
* Each time you add/remove tabs or flags to the JSON config file, you should increase the `fileVersion` number value. This will reset the internally-saved user config and ensure that invalid/nonexistent settings are being loaded.
* The most common mistakes when setting up the JSON file are missing commas and incorrect types of braces. The application will throw an exception when it encounters one of these issues, and will typically tell you a bit of information about it. These errors are pointed out for you in better detail when using a sophisticated code editor like VS Code.

## Examples

Consider the following JSON config file:

	{
	  "fileVersion": 1,
	  "mungedScriptFileName": "modconfig",
	  "userConfigLuaTableName": "gModConfig",
	  "configTabs": [
		{
		  "name": "General",
		  "description": "This page contains general settings that affect the mod && its appearance.",
		  "footnote": "Settings marked with * only affect offline && single-player matches.",
		  "flags": [
			{
			  "path": "cfg_MEUnificationEnabled",
			  "name": "Toggle Mass Effect: Unification",
			  "toolTipCaption": "Whether or not the mod's missions should be added to the game.",
			  "values": [
				"Disabled",
				"Enabled"
			  ],
			  "defaultValue": 1
			},
			{
			  "path": "cfg_CustomHUD",
			  "name": "Custom HUD",
			  "values": [
				"Disabled",
				"Enabled"
			  ],
			  "defaultValue": 1
			}
		  ]
		},
		{
		  "name": "Gameplay",
		  "description": "This page contains settings that affect the mod's gameplay.",
		  "footnote": "Settings marked with * only affect offline && single-player matches.",
		  "flags": [
			{
			  "path": "cfg_Difficulty",
			  "name": "* Difficulty",
			  "values": [
				"Casual",
				"Normal",
				"Veteran",
				"Hardcore",
				"Insanity"
			  ],
			  "defaultValue": 1
			},
			{
			  "path": "cfg_AIHeroes",
			  "name": "* AI Heroes",
			  "values": [
				"Disabled",
				"Enabled"
			  ],
			  "defaultValue": 1
			}
		  ]
		}
	  ]
	}

This would generate the following tabs and dropdown settings:

![Generated settings example](Images/generated-settings-1.png)

![Generated settings example](Images/generated-settings-2.png)

When the player changes a setting value in the app, the value is saved as an integer based on the ordering of the settings in the dropdown, starting at 0.

Example of a Lua script that is generated and munged from a player's config (based on the previous JSON example):

    gModConfig = {
		cfg_MEUnificationEnabled = 1,	-- Enabled
		cfg_CustomHUD = 0,				-- Disabled
		cfg_Difficulty = 1,				-- Normal
		cfg_AIHeroes = 1,				-- Enabled
	}

This would then be munged into a file called `modconfig.script`, which can then be loaded into the game with `ReadDataFile` followed by `ScriptCB_DoFile`. The table's values could then be accessed by `gModConfig.cfg_MEUnificationEnabled` etc.

The file gets re-munged each time "Save Changes" is pressed.

## Implementing

To use the Mod Configurator in your mod, download the [latest release binaries](https://github.com/marth8880/ModConfigurator/releases/latest) and unzip them into a `ConfigUtility` folder someplace in your mod's addon folder. 

Open `config.json` in a text editor (Notepad++ or VS Code recommended) and set up the file as described in the [Setup](#setting-up-a-mod-configuration) and [Examples](#examples) sections. Then, run `ConfigUtility.exe`, which will automatically munge the initial `.SCRIPT` file, which gets generated in the application's folder.

In one of the Lua scripts in your mod, load the `.SCRIPT` file with `ReadDataFile` (the same way you'd load a `LVL` file) followed by `ScriptCB_DoFile` (passing in the name of the `.SCRIPT` file without the file extension). Reference the settings table by the name specified in `userConfigLuaTableName` and the individual setting paths. For example, if the `userConfigLuaTableName` is `gModConfig` and you have a setting whose `path` is `cfg_AIHeroes`, you'd get the value of that setting through `gModConfig.cfg_AIHeroes`. The setting values are numbers that can be easily compared with an if statement.

Please be sure to test each setting before releasing your mod with this tool. The last thing I need is people coming to me because you didn't test your work! With that said, if you ever have any trouble getting anything to work or have any questions at all, please feel free to hit me up on Discord or start a discussion thread here.

### Remarks

* Several measures have been put in place to prevent errors in a mod's config, such as displaying (mostly) useful error messages when needed. It's probably not perfect, but it should prevent most user (and mod author) errors.
* Lua script errors should not be possible, but in case they do arise, the `ScriptMunge.log` file in the `bin` folder will let you know.
