# WinUAE Loader

## About

WinUAE Loader is a WinUAE launcher for arcade cabinets. It supports GameBase Amiga, SPS and WHDLoad game sets and allows you to remap input to suit arcade controls.

## Features

- Map inputs to any arcade control panel layout (default set for IPAC)
- Support for GameBase Amiga, WHDLoad, SPS and DemoBase Amiga game sets
- Global exit key (default ESC)
- Single button Disk Swapper (default 'P')
- Mouse emulation (use Joystick for mouse games)
- Ability to hide CLI when launching WHDLoad games
- Includes databases for the GameEx frontend
- Renames artwork such as title screens, screenshots and box art to match game filenames
- Can update data files dynamically using the GameBase Amiga database
- Run games in the configuration gui for testing

## USAGE

WinUAELoader.exe [-mode [gamebase | whdload  | sps | demobase | auto]] -rom "file"

## Examples

WinUAELoader.exe -mode whdload -rom "NinjaRemix_v1.3.zip"
WinUAELoader.exe -rom "NinjaRemix_v1.3.zip"

Run WinUAELoader.exe to show the configuration gui

The auto mode will attempt to detect the type of game automatically

## Release Notes

- 31-03-2013 - v1.78 - Updated video options
- 24-11-2011 - v1.77 - Integrated UAE Config Creator
- 03-09-2011 - v1.76 - New "-mode auto" command line option for detecting the type of game
- 02-09-2011 - v1.75 - New option under Config tab to disable uae config overwriting
- 01-04-2011 - v1.74 - Enable & Disable Input and Display options
- 10-09-2010 - v1.73 - Added support for editing the .uae files
- 07-09-2009 - v1.72 - Fixed bug in artwork converter and artwork may not be in subfolders
- 24-07-2009 - v1.71 - Fixed command line options for WinUAE 1.6+ (Thanks Greg)
- 16-05-2009 - v1.70 - Added support for DemoBase Amiga
- 21-11-2008 - v1.62 - Force closing of WHDLoad when WinUAE exits
- 07-07-2008 - v1.61 - Removed "-1 Players" from GameEx database
- 01-07-2008 - v1.6 - Updated GameBase Amiga database to 2.6. Added support for GameEx's new database format.
- 16-06-2008 - v1.52 - Separated screen, depth and refresh rates. Added 50Hz (PAL) support.
- 08-05-2008 - v1.51 - Fixed bug when display not set launching from command line
- 30-04-2008 - v1.5 - Added extra display options
- 07-02-2008 - v1.41 - Added variable mouse speed
- 07-02-2008 - v1.4 - Mouse emulation added (use joystick for mouse games)
- 07-02-2008 - v1.39 - Converting artwork now places files in separate folders for GameBase/WHDLoad/SPS
- 05-02-2008 - v1.38 - Fixed input mapping bug
- 03-02-2008 - v1.37 - Added support for Boxscan artwork
- 03-02-2008 - v1.36 - Added filtering for GameEx map generation.
- 03-02-2008 - v1.35 - Can now select from Title Screen, Screenshot 1 or 2 screenshot formats
- 02-02-2008 - v1.34 - Can now convert screenshots without GameBase Amiga database
- 02-02-2008 - v1.33 - Bug fixes
- 01-02-2008 - v1.32 - Changed Input Mapping defaults so there is less chance of a conflict
- 01-02-2008 - v1.31 - Added friendly error message in log when game is not found
- 31-01-2008 - v1.3 - Added single button disk swapper
- 31-01-2008 - v1.21 - Fix for simultaneous games that don't use two joysticks
- 31-01-2008 - v1.2 - Removed "Force Use Mouse". Now you can set any control for port0/port1. If you want the mouse permanently in port0 just set it to Mouse.
- 31-01-2008 - v1.1 - Mouse is now disabled for simultaneous games. Added delete temp files for WHDLoad. Two player game ports are now swapped over. New "Force Use Mouse" option to turn on mouse for all games.
- 30-01-2008 - v1.02 - Fixes to when the control key is used in input mappings
- 30-01-2008 - v1.01 - Fixed control error in databases
- 30-01-2008 - v1.0 - Added auto switching for PAL/NTSC
- 30-01-2008 - v0.91 BETA - Added "Generate Map Files" with ability to remove missing games for GameEx. Key mapping is not turned off when WinUAE is not running.
- 30-01-2008 - v0.90 BETA - First Release

## Notes

WinUAE Loader supports 4 Amiga rom sets

1. GameBase Amiga (4924 roms)
2. KillerGorilla's WHDLoad set (2177 roms)
3. SPS (2028 roms)
4. DemoBase Amiga (18010 roms)

- Get the GameBase Amiga set from http://eab.abime.net/showthread.php?t=23694
- Get the WHDLoad set from http://kgwhd.whdownload.com/
- SPS set can be found on some torrent sites
- DemoBase Amiga set can be found at http://eab.whdownload.com (Username: amiga Password: eab)
- You can find screenshots and dat files @ http://gbamiga.elowar.com/files/ (use CLRMame or ROMCentre to fix your game set)
- You need to place the Kickstart ROMs into WinUAELoader\WHD\Devs\Kickstarts (these are not included)

Copy them to WinUAELoader\WHD\Devs\Kickstarts\ and rename them as follows

- kick34005.A500 <-- Actual ROM (Kickstart v1.3 rev 34.5 (1987)(Commodore)(A500-A1000-A2000-CDTV).rom)
- kick40068.A1200 <-- Actual ROM (Kickstart v3.1 rev 40.68 (1993)(Commodore)(A1200).rom)
- kick40068.A4000 <-- Actual ROM (Kickstart v3.1 rev 40.68 (1993)(Commodore)(A4000).rom)

- The GameEx files you need are in WinUAELoader\Data\GameEx

* The .mdb's go into GameEx\DATA\EMULATORS

- WinUAELoader\Data\GameEx\DATA\EMULATORS\[PC] Commodore Amiga (GameBase).mdb
- WinUAELoader\Data\GameEx\DATA\EMULATORS\[PC] Commodore Amiga (SPS).mdb
- WinUAELoader\Data\GameEx\DATA\EMULATORS\[PC] Commodore Amiga (WHDLoad).mdb
- WinUAELoader\Data\GameEx\DATA\EMULATORS\[PC] Commodore Amiga (DemoBase).mdb

* The .ini's go into GameEx\CONFIG\EMULATORS\IMPORT-EXPORT

- WinUAELoader\Data\GameEx\IMPORT-EXPORT\[PC] Commodore Amiga (GameBase).ini
- WinUAELoader\Data\GameEx\IMPORT-EXPORT\[PC] Commodore Amiga (SPS).ini
- WinUAELoader\Data\GameEx\IMPORT-EXPORT\[PC] Commodore Amiga (WHDLoad).ini
- WinUAELoader\Data\GameEx\IMPORT-EXPORT\[PC] Commodore Amiga (DemoBase).ini

Import them using the Setup Wizard or Advanced Configuration application. Make sure you set your paths correctly.

The default paths assume you have the following file structure

C:\Emulators\WinUAELoader
C:\Emulators\WinUAE

- To set your options in the wrapper run WinUAELoader.exe
- Don't forget to set all your paths
- Use the "Convert Artwork" tool to convert the Screenshots, Title Screens & Boxscan for GameEx
- You can use the "Run Game" under Game Testing in Tools to test games before you run them in GameEx. I suggest you do that before attempting to run them in GameEx.
- You can use the "Input Mappings" to map keys to anything you want. Default settings are for X-Arcade -> IPAC. Note: You need to tick the "Enabled" checkbox to actually turn mappings on for that input profile.
- You don't need to set the "GameBase Amiga Database File" because it's not needed. It's only necessary for the Tools->Generate Game Lists (which is only needed when a new version of the GameBase Amiga database comes out)

## Thanks

Many thanks to the GameBase Amiga (Belgarath / eLowar / KillerGorilla), DemoBase Amiga (Sjakie43) and WHDLoad (Bert Jahn) teams. Without their hard work this program would not be possible. Please support these projects! Also thanks to iano and wobbly for donating a WHDLoad key and DamageCase for help with some WinUAE stuff.

## Website

https://www.baker76.com/winuae-loader/
