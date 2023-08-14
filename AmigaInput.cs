// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;

namespace WinUAELoader
{
	public enum AmKeys : int
	{
		/* First, two dummies */
		AK_mousestuff = 0x100,
		AK_inhibit = 0x101,
		/* This mutates into AK_CTRL in keybuf.c. */
		AK_RCTRL = 0x7f,

		AK_A = 0x20,
		AK_B = 0x35,
		AK_C = 0x33,
		AK_D = 0x22,
		AK_E = 0x12,
		AK_F = 0x23,
		AK_G = 0x24,
		AK_H = 0x25,
		AK_I = 0x17,
		AK_J = 0x26,
		AK_K = 0x27,
		AK_L = 0x28,
		AK_M = 0x37,
		AK_N = 0x36,
		AK_O = 0x18,
		AK_P = 0x19,
		AK_Q = 0x10,
		AK_R = 0x13,
		AK_S = 0x21,
		AK_T = 0x14,
		AK_U = 0x16,
		AK_V = 0x34,
		AK_W = 0x11,
		AK_X = 0x32,
		AK_Y = 0x15,
		AK_Z = 0x31,

		AK_0 = 0x0A,
		AK_1 = 0x01,
		AK_2 = 0x02,
		AK_3 = 0x03,
		AK_4 = 0x04,
		AK_5 = 0x05,
		AK_6 = 0x06,
		AK_7 = 0x07,
		AK_8 = 0x08,
		AK_9 = 0x09,

		AK_NP0 = 0x0F,
		AK_NP1 = 0x1D,
		AK_NP2 = 0x1E,
		AK_NP3 = 0x1F,
		AK_NP4 = 0x2D,
		AK_NP5 = 0x2E,
		AK_NP6 = 0x2F,
		AK_NP7 = 0x3D,
		AK_NP8 = 0x3E,
		AK_NP9 = 0x3F,

		AK_NPDIV = 0x5C,
		AK_NPMUL = 0x5D,
		AK_NPSUB = 0x4A,
		AK_NPADD = 0x5E,
		AK_NPDEL = 0x3C,
		AK_NPLPAREN = 0x5A,
		AK_NPRPAREN = 0x5B,

		AK_F1 = 0x50,
		AK_F2 = 0x51,
		AK_F3 = 0x52,
		AK_F4 = 0x53,
		AK_F5 = 0x54,
		AK_F6 = 0x55,
		AK_F7 = 0x56,
		AK_F8 = 0x57,
		AK_F9 = 0x58,
		AK_F10 = 0x59,

		AK_UP = 0x4C,
		AK_DN = 0x4D,
		AK_LF = 0x4F,
		AK_RT = 0x4E,

		AK_SPC = 0x40,
		AK_BS = 0x41,
		AK_TAB = 0x42,
		AK_ENT = 0x43,
		AK_RET = 0x44,
		AK_ESC = 0x45,
		AK_DEL = 0x46,

		AK_LSH = 0x60,
		AK_RSH = 0x61,
		AK_CAPSLOCK = 0x62,
		AK_CTRL = 0x63,
		AK_LALT = 0x64,
		AK_RALT = 0x65,
		AK_LAMI = 0x66,
		AK_RAMI = 0x67,
		AK_HELP = 0x5F,

		/* The following have different mappings on national keyboards */

		AK_LBRACKET = 0x1A,
		AK_RBRACKET = 0x1B,
		AK_SEMICOLON = 0x29,
		AK_COMMA = 0x38,
		AK_PERIOD = 0x39,
		AK_SLASH = 0x3A,
		AK_BACKSLASH = 0x0D,
		AK_QUOTE = 0x2A,
		AK_NUMBERSIGN = 0x2B,
		AK_LTGT = 0x30,
		AK_BACKQUOTE = 0x00,
		AK_MINUS = 0x0B,
		AK_EQUAL = 0x0C,

		AK_RESETWARNING = 0x78,
		AK_INIT_POWERUP = 0xfd,
		AK_TERM_POWERUP = 0xfe,
	};

	public enum Aks
	{
		AKS_ENTERGUI = 0x200, AKS_SCREENSHOT_FILE, AKS_SCREENSHOT_CLIPBOARD, AKS_FREEZEBUTTON,
		AKS_FLOPPY0, AKS_FLOPPY1, AKS_FLOPPY2, AKS_FLOPPY3,
		AKS_EFLOPPY0, AKS_EFLOPPY1, AKS_EFLOPPY2, AKS_EFLOPPY3,
		AKS_TOGGLEDEFAULTSCREEN,
		AKS_TOGGLEWINDOWEDFULLSCREEN, AKS_TOGGLEFULLWINDOWFULLSCREEN, AKS_TOGGLEWINDOWFULLWINDOW,
		AKS_ENTERDEBUGGER, AKS_IRQ7,
		AKS_PAUSE, AKS_WARP, AKS_INHIBITSCREEN,
		AKS_STATEREWIND, AKS_STATECURRENT, AKS_STATECAPTURE,
		AKS_VIDEORECORD,
		AKS_VOLDOWN, AKS_VOLUP, AKS_VOLMUTE,
		AKS_MVOLDOWN, AKS_MVOLUP, AKS_MVOLMUTE,
		AKS_QUIT, AKS_HARDRESET, AKS_SOFTRESET,
		AKS_STATESAVEQUICK, AKS_STATERESTOREQUICK,
		AKS_STATESAVEQUICK1, AKS_STATERESTOREQUICK1,
		AKS_STATESAVEQUICK2, AKS_STATERESTOREQUICK2,
		AKS_STATESAVEQUICK3, AKS_STATERESTOREQUICK3,
		AKS_STATESAVEQUICK4, AKS_STATERESTOREQUICK4,
		AKS_STATESAVEQUICK5, AKS_STATERESTOREQUICK5,
		AKS_STATESAVEQUICK6, AKS_STATERESTOREQUICK6,
		AKS_STATESAVEQUICK7, AKS_STATERESTOREQUICK7,
		AKS_STATESAVEQUICK8, AKS_STATERESTOREQUICK8,
		AKS_STATESAVEQUICK9, AKS_STATERESTOREQUICK9,
		AKS_STATESAVEDIALOG, AKS_STATERESTOREDIALOG,
		AKS_DECREASEREFRESHRATE,
		AKS_INCREASEREFRESHRATE,
		AKS_ARCADIADIAGNOSTICS, AKS_ARCADIAPLY1, AKS_ARCADIAPLY2, AKS_ARCADIACOIN1, AKS_ARCADIACOIN2,
		AKS_TOGGLEMOUSEGRAB, AKS_SWITCHINTERPOL, AKS_TOGGLERTG,
		AKS_INPUT_CONFIG_1, AKS_INPUT_CONFIG_2, AKS_INPUT_CONFIG_3, AKS_INPUT_CONFIG_4,
		AKS_DISKSWAPPER_NEXT, AKS_DISKSWAPPER_PREV,
		AKS_DISKSWAPPER_INSERT0, AKS_DISKSWAPPER_INSERT1, AKS_DISKSWAPPER_INSERT2, AKS_DISKSWAPPER_INSERT3,
		AKS_DISK_PREV0, AKS_DISK_PREV1, AKS_DISK_PREV2, AKS_DISK_PREV3,
		AKS_DISK_NEXT0, AKS_DISK_NEXT1, AKS_DISK_NEXT2, AKS_DISK_NEXT3,
		AKS_CDTV_FRONT_PANEL_STOP, AKS_CDTV_FRONT_PANEL_PLAYPAUSE, AKS_CDTV_FRONT_PANEL_PREV,
		AKS_CDTV_FRONT_PANEL_NEXT, AKS_CDTV_FRONT_PANEL_REW, AKS_CDTV_FRONT_PANEL_FF,
		AKS_QUALIFIER1, AKS_QUALIFIER2, AKS_QUALIFIER3, AKS_QUALIFIER4,
		AKS_QUALIFIER5, AKS_QUALIFIER6, AKS_QUALIFIER7, AKS_QUALIFIER8,
		AKS_QUALIFIER_SPECIAL, AKS_QUALIFIER_SHIFT, AKS_QUALIFIER_CONTROL,
		AKS_QUALIFIER_ALT, AKS_QUALIFIER_WIN
	};

	class AmigaKeyDefaultNode
	{
		public int DIKKey = 0;
		public string Name = null;

		public AmigaKeyDefaultNode(int dikKey, string name)
		{
			DIKKey = dikKey;
			Name = name;
		}
	}

	class AmigaKeyNode
	{
		public string ConfigName = null;
		public string Name = null;
		public int AllowMask = 0;
		public int Type = 0;
		public int Unit = 0;
		public int Data = 0;

		public AmigaKeyNode(string configName, string name, int allowMask, int type, int unit, int data)
		{
			ConfigName = configName;
			Name = name;
			AllowMask = allowMask;
			Type = type;
			Unit = unit;
			Data = data;
		}
	}

	class AmigaInput
	{
		private const int DIR_LEFT = 1;
		private const int DIR_RIGHT = 2;
		private const int DIR_UP = 4;
		private const int DIR_DOWN = 8;

		private const int IE_INVERT = 0x80;
		private const int IE_CDTV = 0x100;

		private const int JOYBUTTON_1 = 0; /* fire/left mousebutton */
		private const int JOYBUTTON_2 = 1; /* 2nd/right mousebutton */
		private const int JOYBUTTON_3 = 2; /* 3rd/middle mousebutton */
		private const int JOYBUTTON_CD32_PLAY = 3;
		private const int JOYBUTTON_CD32_RWD = 4;
		private const int JOYBUTTON_CD32_FFW = 5;
		private const int JOYBUTTON_CD32_GREEN = 6;
		private const int JOYBUTTON_CD32_YELLOW = 7;
		private const int JOYBUTTON_CD32_RED = 8;
		private const int JOYBUTTON_CD32_BLUE = 9;

		private const int AM_KEY = 1; /* keyboard allowed */
		private const int AM_JOY_BUT = 2; /* joystick buttons allowed */
		private const int AM_JOY_AXIS = 4; /* joystick axis allowed */
		private const int AM_MOUSE_BUT = 8; /* mouse buttons allowed */
		private const int AM_MOUSE_AXIS = 16; /* mouse direction allowed */
		private const int AM_AF = 32; /* supports autofire */
		private const int AM_INFO = 64; /* information data for gui */
		private const int AM_DUMMY = 128; /* placeholder */
		private const int AM_CUSTOM = 256; /* custom event */
		private const int AM_SETTOGGLE = 512; /* on/off/toggle */
		private const int AM_K = (AM_KEY | AM_JOY_BUT | AM_MOUSE_BUT | AM_AF); /* generic button/switch */
		private const int AM_KK = (AM_KEY | AM_JOY_BUT | AM_MOUSE_BUT);
		private const int AM_KT = (AM_K | AM_SETTOGGLE);

		// KB LED stuff
		private const int KBLED_NUMLOCKB = 0;
		private const int KBLED_CAPSLOCKB = 1;
		private const int KBLED_SCROLLLOCKB = 2;

		private const int KBLED_NUMLOCKM = (1 << KBLED_NUMLOCKB);
		private const int KBLED_CAPSLOCKM = (1 << KBLED_CAPSLOCKB);
		private const int KBLED_SCROLLLOCKM = (1 << KBLED_SCROLLLOCKB);

		public static Dictionary<string, AmigaKeyNode> AmigaKeyHash = null;

		public static AmigaKeyNode[] AmigaKeys =
        {
            /* joystick/mouse port 1 */

            new AmigaKeyNode("JOYPORT1_START","Joystick port 1", AM_INFO, 0,1,0),

            new AmigaKeyNode("MOUSE1_FIRST", "", AM_DUMMY, 0,0,0),

            new AmigaKeyNode("MOUSE1_HORIZ","Mouse1 Horizontal",AM_MOUSE_AXIS|AM_JOY_AXIS,8,1,0),
            new AmigaKeyNode("MOUSE1_VERT","Mouse1 Vertical",AM_MOUSE_AXIS|AM_JOY_AXIS,8,1,1),
            new AmigaKeyNode("MOUSE1_HORIZ_INV","Mouse1 Horizontal (inverted)",AM_MOUSE_AXIS|AM_JOY_AXIS,8,1,0|IE_INVERT),
            new AmigaKeyNode("MOUSE1_VERT_INV","Mouse1 Vertical (inverted)",AM_MOUSE_AXIS|AM_JOY_AXIS,8,1,1|IE_INVERT),
			new AmigaKeyNode("MOUSE1_UP","Mouse1 Up",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,1,DIR_UP),
            new AmigaKeyNode("MOUSE1_DOWN","Mouse1 Down",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,1,DIR_DOWN),
            new AmigaKeyNode("MOUSE1_LEFT","Mouse1 Left",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,1,DIR_LEFT),
            new AmigaKeyNode("MOUSE1_RIGHT","Mouse1 Right",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,1,DIR_RIGHT),
            new AmigaKeyNode("MOUSE1_WHEEL","Mouse1 Wheel",AM_MOUSE_AXIS|AM_JOY_AXIS,8,1,2),

            new AmigaKeyNode("MOUSE1_LAST", "", AM_DUMMY, 0,0,0),
   
			new AmigaKeyNode("MOUSE_CDTV_HORIZ","CDTV Mouse Horizontal",AM_JOY_AXIS,8,1,0|IE_CDTV),
			new AmigaKeyNode("MOUSE_CDTV_VERT","CDTV Mouse Vertical",AM_JOY_AXIS,8,1,1|IE_CDTV),
			new AmigaKeyNode("MOUSE_CDTV_UP","CDTV Mouse Up",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,1,DIR_UP|IE_CDTV),
			new AmigaKeyNode("MOUSE_CDTV_DOWN","CDTV Mouse Down",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,1,DIR_DOWN|IE_CDTV),
			new AmigaKeyNode("MOUSE_CDTV_LEFT","CDTV Mouse Left",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,1,DIR_LEFT|IE_CDTV),
			new AmigaKeyNode("MOUSE_CDTV_RIGHT","CDTV Mouse Right",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,1,DIR_RIGHT|IE_CDTV),
			
            new AmigaKeyNode("JOY1_HORIZ","Joy1 Horizontal",AM_JOY_AXIS,0,1,DIR_LEFT|DIR_RIGHT),
            new AmigaKeyNode("JOY1_VERT","Joy1 Vertical",AM_JOY_AXIS,0,1,DIR_UP|DIR_DOWN),
            new AmigaKeyNode("JOY1_HORIZ_POT","Joy1 Horizontal (Analog)",AM_JOY_AXIS,128,1,1),
            new AmigaKeyNode("JOY1_VERT_POT","Joy1 Vertical (Analog)",AM_JOY_AXIS,128,1,0),
            new AmigaKeyNode("JOY1_HORIZ_POT_INV","Joy1 Horizontal (Analog, inverted)",AM_JOY_AXIS,128,1,1|IE_INVERT),
            new AmigaKeyNode("JOY1_VERT_POT_INV","Joy1 Vertical (Analog, inverted)",AM_JOY_AXIS,128,1,0|IE_INVERT),

            new AmigaKeyNode("JOY1_LEFT","Joy1 Left",AM_K,16,1,DIR_LEFT),
            new AmigaKeyNode("JOY1_RIGHT","Joy1 Right",AM_K,16,1,DIR_RIGHT),
            new AmigaKeyNode("JOY1_UP","Joy1 Up",AM_K,16,1,DIR_UP),
            new AmigaKeyNode("JOY1_DOWN","Joy1 Down",AM_K,16,1,DIR_DOWN),
            new AmigaKeyNode("JOY1_LEFT_UP","Joy1 Left+Up",AM_K,16,1,DIR_LEFT|DIR_UP),
            new AmigaKeyNode("JOY1_LEFT_DOWN","Joy1 Left+Down",AM_K,16,1,DIR_LEFT|DIR_DOWN),
            new AmigaKeyNode("JOY1_RIGHT_UP","Joy1 Right+Up",AM_K,16,1,DIR_RIGHT|DIR_UP),
            new AmigaKeyNode("JOY1_RIGHT_DOWN","Joy1 Right+Down",AM_K,16,1,DIR_RIGHT|DIR_DOWN),

            new AmigaKeyNode("JOY1_FIRE_BUTTON","Joy1 Fire/Mouse1 Left Button",AM_K,4,1,JOYBUTTON_1),
            new AmigaKeyNode("JOY1_2ND_BUTTON","Joy1 2nd Button/Mouse1 Right Button",AM_K,4,1,JOYBUTTON_2),
            new AmigaKeyNode("JOY1_3RD_BUTTON","Joy1 3rd Button/Mouse1 Middle Button",AM_K,4,1,JOYBUTTON_3),
            new AmigaKeyNode("JOY1_CD32_PLAY","Joy1 CD32 Play",AM_K,4,1,JOYBUTTON_CD32_PLAY),
            new AmigaKeyNode("JOY1_CD32_RWD","Joy1 CD32 RWD",AM_K,4,1,JOYBUTTON_CD32_RWD),
            new AmigaKeyNode("JOY1_CD32_FFW","Joy1 CD32 FFW",AM_K,4,1,JOYBUTTON_CD32_FFW),
            new AmigaKeyNode("JOY1_CD32_GREEN","Joy1 CD32 Green",AM_K,4,1,JOYBUTTON_CD32_GREEN),
            new AmigaKeyNode("JOY1_CD32_YELLOW","Joy1 CD32 Yellow",AM_K,4,1,JOYBUTTON_CD32_YELLOW),
            new AmigaKeyNode("JOY1_CD32_RED","Joy1 CD32 Red",AM_K,4,1,JOYBUTTON_CD32_RED),
            new AmigaKeyNode("JOY1_CD32_BLUE","Joy1 CD32 Blue",AM_K,4,1,JOYBUTTON_CD32_BLUE),

            /* joystick/mouse port 2 */

            new AmigaKeyNode("JOYPORT2_START","Joystick port 2", AM_INFO, 0,2,0),

            new AmigaKeyNode("MOUSE2_FIRST", "", AM_DUMMY, 0,0,0),

            new AmigaKeyNode("MOUSE2_HORIZ","Mouse2 Horizontal",AM_MOUSE_AXIS|AM_JOY_AXIS,8,2,0),
            new AmigaKeyNode("MOUSE2_VERT","Mouse2 Vertical",AM_MOUSE_AXIS|AM_JOY_AXIS,8,2,1),
            new AmigaKeyNode("MOUSE2_HORIZ_INV","Mouse2 Horizontal (inverted)",AM_MOUSE_AXIS|AM_JOY_AXIS,8,2,0|IE_INVERT),
            new AmigaKeyNode("MOUSE2_VERT_INV","Mouse2 Vertical (inverted)",AM_MOUSE_AXIS|AM_JOY_AXIS,8,2,1|IE_INVERT),
			new AmigaKeyNode("MOUSE2_UP","Mouse2 Up",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,2,DIR_UP),
            new AmigaKeyNode("MOUSE2_DOWN","Mouse2 Down",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,32,2,DIR_DOWN),
            new AmigaKeyNode("MOUSE2_LEFT","Mouse2 Left",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,2,DIR_LEFT),
            new AmigaKeyNode("MOUSE2_RIGHT","Mouse2 Right",AM_KEY|AM_JOY_BUT|AM_MOUSE_BUT,64,2,DIR_RIGHT),

            new AmigaKeyNode("MOUSE2_LAST", "", AM_DUMMY, 0,0,0),

            new AmigaKeyNode("JOY2_HORIZ","Joy2 Horizontal",AM_JOY_AXIS,0,2,DIR_LEFT|DIR_RIGHT),
            new AmigaKeyNode("JOY2_VERT","Joy2 Vertical",AM_JOY_AXIS,0,2,DIR_UP|DIR_DOWN),
            new AmigaKeyNode("JOY2_HORIZ_POT","Joy2 Horizontal (Analog)",AM_JOY_AXIS,128,2,1),
            new AmigaKeyNode("JOY2_VERT_POT","Joy2 Vertical (Analog)",AM_JOY_AXIS,128,2,0),
            new AmigaKeyNode("JOY2_HORIZ_POT_INV","Joy2 Horizontal (Analog, inverted)",AM_JOY_AXIS,128,2,1|IE_INVERT),
            new AmigaKeyNode("JOY2_VERT_POT_INV","Joy2 Vertical (Analog, inverted)",AM_JOY_AXIS,128,2,0|IE_INVERT),

            new AmigaKeyNode("JOY2_LEFT","Joy2 Left",AM_K,16,2,DIR_LEFT),
            new AmigaKeyNode("JOY2_RIGHT","Joy2 Right",AM_K,16,2,DIR_RIGHT),
            new AmigaKeyNode("JOY2_UP","Joy2 Up",AM_K,16,2,DIR_UP),
            new AmigaKeyNode("JOY2_DOWN","Joy2 Down",AM_K,16,2,DIR_DOWN),
            new AmigaKeyNode("JOY2_LEFT_UP","Joy2 Left+Up",AM_K,16,2,DIR_LEFT|DIR_UP),
            new AmigaKeyNode("JOY2_LEFT_DOWN","Joy2 Left+Down",AM_K,16,2,DIR_LEFT|DIR_DOWN),
            new AmigaKeyNode("JOY2_RIGHT_UP","Joy2 Right+Up",AM_K,16,2,DIR_RIGHT|DIR_UP),
            new AmigaKeyNode("JOY2_RIGHT_DOWN","Joy2 Right+Down",AM_K,16,2,DIR_RIGHT|DIR_DOWN),

            new AmigaKeyNode("JOY2_FIRE_BUTTON","Joy2 Fire/Mouse2 Left Button",AM_K,4,2,JOYBUTTON_1),
            new AmigaKeyNode("JOY2_2ND_BUTTON","Joy2 2nd Button/Mouse2 Right Button",AM_K,4,2,JOYBUTTON_2),
            new AmigaKeyNode("JOY2_3RD_BUTTON","Joy2 3rd Button/Mouse2 Middle Button",AM_K,4,2,JOYBUTTON_3),
            new AmigaKeyNode("JOY2_CD32_PLAY","Joy2 CD32 Play",AM_K,4,2,JOYBUTTON_CD32_PLAY),
            new AmigaKeyNode("JOY2_CD32_RWD","Joy2 CD32 RWD",AM_K,4,2,JOYBUTTON_CD32_RWD),
            new AmigaKeyNode("JOY2_CD32_FFW","Joy2 CD32 FFW",AM_K,4,2,JOYBUTTON_CD32_FFW),
            new AmigaKeyNode("JOY2_CD32_GREEN","Joy2 CD32 Green",AM_K,4,2,JOYBUTTON_CD32_GREEN),
            new AmigaKeyNode("JOY2_CD32_YELLOW","Joy2 CD32 Yellow",AM_K,4,2,JOYBUTTON_CD32_YELLOW),
            new AmigaKeyNode("JOY2_CD32_RED","Joy2 CD32 Red",AM_K,4,2,JOYBUTTON_CD32_RED),
            new AmigaKeyNode("JOY2_CD32_BLUE","Joy2 CD32 Blue",AM_K,4,2,JOYBUTTON_CD32_BLUE),

			new AmigaKeyNode("LIGHTPEN_FIRST", "", AM_DUMMY, 0,0,0), 
			
			new AmigaKeyNode("LIGHTPEN_HORIZ","Lightpen Horizontal",AM_MOUSE_AXIS|AM_JOY_AXIS,0,5,0),
			new AmigaKeyNode("LIGHTPEN_VERT","Lightpen Vertical",AM_MOUSE_AXIS|AM_JOY_AXIS,0,5,1),
			new AmigaKeyNode("LIGHTPEN_LEFT","Lightpen Left",AM_K,1,5,DIR_LEFT),
			new AmigaKeyNode("LIGHTPEN_RIGHT","Lightpen Right",AM_K,1,5,DIR_RIGHT),
			new AmigaKeyNode("LIGHTPEN_UP","Lightpen Up",AM_K,1,5,DIR_UP),
			new AmigaKeyNode("LIGHTPEN_DOWN","Lightpen Down",AM_K,1,5,DIR_DOWN),

			new AmigaKeyNode("LIGHTPEN_LAST", "", AM_DUMMY, 0,0,0), 	
			
            /* parallel port joystick adapter */

            new AmigaKeyNode("PAR_JOY1_START", "Parallel port joystick adapter", AM_INFO, 0,3,0),

            new AmigaKeyNode("PAR_JOY1_HORIZ","Parallel Joy1 Horizontal",AM_JOY_AXIS,0,3,DIR_LEFT|DIR_RIGHT),
            new AmigaKeyNode("PAR_JOY1_VERT","Parallel Joy1 Vertical",AM_JOY_AXIS,0,3,DIR_UP|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY1_LEFT","Parallel Joy1 Left",AM_K,16,3,DIR_LEFT),
            new AmigaKeyNode("PAR_JOY1_RIGHT","Parallel Joy1 Right",AM_K,16,3,DIR_RIGHT),
            new AmigaKeyNode("PAR_JOY1_UP","Parallel Joy1 Up",AM_K,16,3,DIR_UP),
            new AmigaKeyNode("PAR_JOY1_DOWN","Parallel Joy1 Down",AM_K,16,3,DIR_DOWN),
            new AmigaKeyNode("PAR_JOY1_LEFT_UP","Parallel Joy1 Left+Up",AM_K,16,3,DIR_LEFT|DIR_UP),
            new AmigaKeyNode("PAR_JOY1_LEFT_DOWN","Parallel Joy1 Left+Down",AM_K,16,3,DIR_LEFT|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY1_RIGHT_UP","Parallel Joy1 Right+Up",AM_K,16,3,DIR_RIGHT|DIR_UP),
            new AmigaKeyNode("PAR_JOY1_RIGHT_DOWN","Parallel Joy1 Right+Down",AM_K,16,3,DIR_RIGHT|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY1_FIRE_BUTTON","Parallel Joy1 Fire Button",AM_K,4,3,JOYBUTTON_1),

            new AmigaKeyNode("PAR_JOY2_START", "", AM_DUMMY, 0,4,0),

            new AmigaKeyNode("PAR_JOY2_HORIZ","Parallel Joy2 Horizontal",AM_JOY_AXIS,0,4,DIR_LEFT|DIR_RIGHT),
            new AmigaKeyNode("PAR_JOY2_VERT","Parallel Joy2 Vertical",AM_JOY_AXIS,0,4,DIR_UP|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY2_LEFT","Parallel Joy2 Left",AM_K,16,4,DIR_LEFT),
            new AmigaKeyNode("PAR_JOY2_RIGHT","Parallel Joy2 Right",AM_K,16,4,DIR_RIGHT),
            new AmigaKeyNode("PAR_JOY2_UP","Parallel Joy2 Up",AM_K,16,4,DIR_UP),
            new AmigaKeyNode("PAR_JOY2_DOWN","Parallel Joy2 Down",AM_K,16,4,DIR_DOWN),
            new AmigaKeyNode("PAR_JOY2_LEFT_UP","Parallel Joy2 Left+Up",AM_K,16,4,DIR_LEFT|DIR_UP),
            new AmigaKeyNode("PAR_JOY2_LEFT_DOWN","Parallel Joy2 Left+Down",AM_K,16,4,DIR_LEFT|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY2_RIGHT_UP","Parallel Joy2 Right+Up",AM_K,16,4,DIR_RIGHT|DIR_UP),
            new AmigaKeyNode("PAR_JOY2_RIGHT_DOWN","Parallel Joy2 Right+Down",AM_K,16,4,DIR_RIGHT|DIR_DOWN),
            new AmigaKeyNode("PAR_JOY2_FIRE_BUTTON","Parallel Joy2 Fire Button",AM_K,4,4,JOYBUTTON_1),
			new AmigaKeyNode("PAR_JOY2_2ND_BUTTON","Parallel Joy2 Spare/2nd Button",AM_K,4,4,JOYBUTTON_2),

            new AmigaKeyNode("PAR_JOY_END", "", AM_DUMMY, 0,0,0),

			/* qualifiers */
			new AmigaKeyNode("SPC_QUALIFIER_START", "Qualifiers", AM_INFO, 0,0,0),
			
			new AmigaKeyNode("SPC_QUALIFIER1", "Qualifier 1", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER1),
			new AmigaKeyNode("SPC_QUALIFIER2", "Qualifier 2", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER2),
			new AmigaKeyNode("SPC_QUALIFIER3", "Qualifier 3", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER3),
			new AmigaKeyNode("SPC_QUALIFIER4", "Qualifier 4", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER4),
			new AmigaKeyNode("SPC_QUALIFIER5", "Qualifier 5", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER5),
			new AmigaKeyNode("SPC_QUALIFIER6", "Qualifier 6", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER6),
			new AmigaKeyNode("SPC_QUALIFIER7", "Qualifier 7", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER7),
			new AmigaKeyNode("SPC_QUALIFIER8", "Qualifier 8", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER8),
			new AmigaKeyNode("SPC_QUALIFIER_SPECIAL", "Qualifier Special", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER_SPECIAL),
			new AmigaKeyNode("SPC_QUALIFIER_SHIFT", "Qualifier Shift", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER_SHIFT),
			new AmigaKeyNode("SPC_QUALIFIER_CONTROL", "Qualifier Control", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER_CONTROL),
			new AmigaKeyNode("SPC_QUALIFIER_ALT", "Qualifier Alt", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER_ALT),
			new AmigaKeyNode("SPC_QUALIFIER_WIN", "Qualifier Win", AM_KK, 0,0, (int)Aks.AKS_QUALIFIER_WIN),
		
			new AmigaKeyNode("SPC_QUALIFIER_END", "", AM_DUMMY, 0,0,0),
			
            /* keys */

            new AmigaKeyNode("KEY_START","Keyboard",AM_INFO, 0,0,0),

            new AmigaKeyNode("KEY_F1","F1",AM_K,0,0,(int)AmKeys.AK_F1),
            new AmigaKeyNode("KEY_F2","F2",AM_K,0,0,(int)AmKeys.AK_F2),
            new AmigaKeyNode("KEY_F3","F3",AM_K,0,0,(int)AmKeys.AK_F3),
            new AmigaKeyNode("KEY_F4","F4",AM_K,0,0,(int)AmKeys.AK_F4),
            new AmigaKeyNode("KEY_F5","F5",AM_K,0,0,(int)AmKeys.AK_F5),
            new AmigaKeyNode("KEY_F6","F6",AM_K,0,0,(int)AmKeys.AK_F6),
            new AmigaKeyNode("KEY_F7","F7",AM_K,0,0,(int)AmKeys.AK_F7),
            new AmigaKeyNode("KEY_F8","F8",AM_K,0,0,(int)AmKeys.AK_F8),
            new AmigaKeyNode("KEY_F9","F9",AM_K,0,0,(int)AmKeys.AK_F9),
            new AmigaKeyNode("KEY_F10","F10",AM_K,0,0,(int)AmKeys.AK_F10),

            /* "special" keys */

            new AmigaKeyNode("KEY_ESC","ESC",AM_K,0,0,(int)AmKeys.AK_ESC),
            new AmigaKeyNode("KEY_TAB","Tab",AM_K,0,0,(int)AmKeys.AK_TAB),
            new AmigaKeyNode("KEY_CTRL","CTRL",AM_K,0,0,(int)AmKeys.AK_CTRL),
            new AmigaKeyNode("KEY_CTRL_RIGHT","Right CTRL",AM_K,0,0,(int)AmKeys.AK_RCTRL), // no longer in inputevents
            new AmigaKeyNode("KEY_CAPS_LOCK","Caps Lock",AM_K,0,0,(int)AmKeys.AK_CAPSLOCK),
            new AmigaKeyNode("KEY_SHIFT_LEFT","Left Shift",AM_K,0,0,(int)AmKeys.AK_LSH),
            new AmigaKeyNode("KEY_ALT_LEFT","Left Alt",AM_K,0,0,(int)AmKeys.AK_LALT),
            new AmigaKeyNode("KEY_AMIGA_LEFT","Left Amiga",AM_K,0,0,(int)AmKeys.AK_LAMI),
            new AmigaKeyNode("KEY_AMIGA_RIGHT","Right Amiga",AM_K,0,0,(int)AmKeys.AK_RAMI),
            new AmigaKeyNode("KEY_ALT_RIGHT","Right Alt",AM_K,0,0,(int)AmKeys.AK_RALT),
            new AmigaKeyNode("KEY_SHIFT_RIGHT","Right Shift",AM_K,0,0,(int)AmKeys.AK_RSH),
            new AmigaKeyNode("KEY_SPACE","Space",AM_K,0,0,(int)AmKeys.AK_SPC),
            new AmigaKeyNode("KEY_CURSOR_UP","Cursor Up",AM_K,0,0,(int)AmKeys.AK_UP),
            new AmigaKeyNode("KEY_CURSOR_DOWN","Cursor Down",AM_K,0,0,(int)AmKeys.AK_DN),
            new AmigaKeyNode("KEY_CURSOR_LEFT","Cursor Left",AM_K,0,0,(int)AmKeys.AK_LF),
            new AmigaKeyNode("KEY_CURSOR_RIGHT","Cursor Right",AM_K,0,0,(int)AmKeys.AK_RT),
            new AmigaKeyNode("KEY_HELP","Help",AM_K,0,0,(int)AmKeys.AK_HELP),
            new AmigaKeyNode("KEY_DEL","Del",AM_K,0,0,(int)AmKeys.AK_DEL),
            new AmigaKeyNode("KEY_BACKSPACE","Backspace",AM_K,0,0,(int)AmKeys.AK_BS),
            new AmigaKeyNode("KEY_RETURN","Return",AM_K,0,0,(int)AmKeys.AK_RET),

            new AmigaKeyNode("KEY_A","A",AM_K,0,0,(int)AmKeys.AK_A),
            new AmigaKeyNode("KEY_B","B",AM_K,0,0,(int)AmKeys.AK_B),
            new AmigaKeyNode("KEY_C","C",AM_K,0,0,(int)AmKeys.AK_C),
            new AmigaKeyNode("KEY_D","D",AM_K,0,0,(int)AmKeys.AK_D),
            new AmigaKeyNode("KEY_E","E",AM_K,0,0,(int)AmKeys.AK_E),
            new AmigaKeyNode("KEY_F","F",AM_K,0,0,(int)AmKeys.AK_F),
            new AmigaKeyNode("KEY_G","G",AM_K,0,0,(int)AmKeys.AK_G),
            new AmigaKeyNode("KEY_H","H",AM_K,0,0,(int)AmKeys.AK_H),
            new AmigaKeyNode("KEY_I","I",AM_K,0,0,(int)AmKeys.AK_I),
            new AmigaKeyNode("KEY_J","J",AM_K,0,0,(int)AmKeys.AK_J),
            new AmigaKeyNode("KEY_K","K",AM_K,0,0,(int)AmKeys.AK_K),
            new AmigaKeyNode("KEY_L","L",AM_K,0,0,(int)AmKeys.AK_L),
            new AmigaKeyNode("KEY_M","M",AM_K,0,0,(int)AmKeys.AK_M),
            new AmigaKeyNode("KEY_N","N",AM_K,0,0,(int)AmKeys.AK_N),
            new AmigaKeyNode("KEY_O","O",AM_K,0,0,(int)AmKeys.AK_O),
            new AmigaKeyNode("KEY_P","P",AM_K,0,0,(int)AmKeys.AK_P),
            new AmigaKeyNode("KEY_Q","Q",AM_K,0,0,(int)AmKeys.AK_Q),
            new AmigaKeyNode("KEY_R","R",AM_K,0,0,(int)AmKeys.AK_R),
            new AmigaKeyNode("KEY_S","S",AM_K,0,0,(int)AmKeys.AK_S),
            new AmigaKeyNode("KEY_T","T",AM_K,0,0,(int)AmKeys.AK_T),
            new AmigaKeyNode("KEY_U","U",AM_K,0,0,(int)AmKeys.AK_U),
            new AmigaKeyNode("KEY_V","V",AM_K,0,0,(int)AmKeys.AK_V),
            new AmigaKeyNode("KEY_W","W",AM_K,0,0,(int)AmKeys.AK_W),
            new AmigaKeyNode("KEY_X","X",AM_K,0,0,(int)AmKeys.AK_X),
            new AmigaKeyNode("KEY_Y","Y",AM_K,0,0,(int)AmKeys.AK_Y),
            new AmigaKeyNode("KEY_Z","Z",AM_K,0,0,(int)AmKeys.AK_Z),

            /* numpad */			
			
            new AmigaKeyNode("KEY_ENTER","Numpad Enter",AM_K,0,0,(int)AmKeys.AK_ENT),
            new AmigaKeyNode("KEY_NP_0","Numpad 0",AM_K,0,0,(int)AmKeys.AK_NP0),
            new AmigaKeyNode("KEY_NP_1","Numpad 1",AM_K,0,0,(int)AmKeys.AK_NP1),
            new AmigaKeyNode("KEY_NP_2","Numpad 2",AM_K,0,0,(int)AmKeys.AK_NP2),
            new AmigaKeyNode("KEY_NP_3","Numpad 3",AM_K,0,0,(int)AmKeys.AK_NP3),
            new AmigaKeyNode("KEY_NP_4","Numpad 4",AM_K,0,0,(int)AmKeys.AK_NP4),
            new AmigaKeyNode("KEY_NP_5","Numpad 5",AM_K,0,0,(int)AmKeys.AK_NP5),
            new AmigaKeyNode("KEY_NP_6","Numpad 6",AM_K,0,0,(int)AmKeys.AK_NP6),
            new AmigaKeyNode("KEY_NP_7","Numpad 7",AM_K,0,0,(int)AmKeys.AK_NP7),
            new AmigaKeyNode("KEY_NP_8","Numpad 8",AM_K,0,0,(int)AmKeys.AK_NP8),
            new AmigaKeyNode("KEY_NP_9","Numpad 9",AM_K,0,0,(int)AmKeys.AK_NP9),
            new AmigaKeyNode("KEY_NP_PERIOD","Numpad Period",AM_K,0,0,(int)AmKeys.AK_NPDEL),
            new AmigaKeyNode("KEY_NP_ADD","Numpad Plus",AM_K,0,0,(int)AmKeys.AK_NPADD),
            new AmigaKeyNode("KEY_NP_SUB","Numpad Minus",AM_K,0,0,(int)AmKeys.AK_NPSUB),
            new AmigaKeyNode("KEY_NP_MUL","Numpad Multiply",AM_K,0,0,(int)AmKeys.AK_NPMUL),
            new AmigaKeyNode("KEY_NP_DIV","Numpad Divide",AM_K,0,0,(int)AmKeys.AK_NPDIV),
            new AmigaKeyNode("KEY_NP_LPAREN","Numpad Left Parenthesis",AM_K,0,0,(int)AmKeys.AK_NPLPAREN),
            new AmigaKeyNode("KEY_NP_RPAREN","Numpad Right Parenthesis",AM_K,0,0,(int)AmKeys.AK_NPRPAREN),
            new AmigaKeyNode("KEY_2B","Keycode 0x2b",AM_K,0,0,0x2b),
            new AmigaKeyNode("KEY_30","Keycode 0x30",AM_K,0,0,0x30),

            new AmigaKeyNode("KEY_BACKQUOTE","Back Quote",AM_K,0,0,(int)AmKeys.AK_BACKQUOTE),
            new AmigaKeyNode("KEY_1","1",AM_K,0,0,(int)AmKeys.AK_1),
            new AmigaKeyNode("KEY_2","2",AM_K,0,0,(int)AmKeys.AK_2),
            new AmigaKeyNode("KEY_3","3",AM_K,0,0,(int)AmKeys.AK_3),
            new AmigaKeyNode("KEY_4","4",AM_K,0,0,(int)AmKeys.AK_4),
            new AmigaKeyNode("KEY_5","5",AM_K,0,0,(int)AmKeys.AK_5),
            new AmigaKeyNode("KEY_6","6",AM_K,0,0,(int)AmKeys.AK_6),
            new AmigaKeyNode("KEY_7","7",AM_K,0,0,(int)AmKeys.AK_7),
            new AmigaKeyNode("KEY_8","8",AM_K,0,0,(int)AmKeys.AK_8),
            new AmigaKeyNode("KEY_9","9",AM_K,0,0,(int)AmKeys.AK_9),
            new AmigaKeyNode("KEY_0","0",AM_K,0,0,(int)AmKeys.AK_0),
            new AmigaKeyNode("KEY_SUB","Minus",AM_K,0,0,(int)AmKeys.AK_MINUS),
            new AmigaKeyNode("KEY_EQUALS","Equals",AM_K,0,0,(int)AmKeys.AK_EQUAL),
            new AmigaKeyNode("KEY_BACKSLASH","Backslash",AM_K,0,0,(int)AmKeys.AK_BACKSLASH),

            new AmigaKeyNode("KEY_LEFTBRACKET","Left Bracket",AM_K,0,0,(int)AmKeys.AK_LBRACKET),
            new AmigaKeyNode("KEY_RIGHTBRACKET","Right Bracket",AM_K,0,0,(int)AmKeys.AK_RBRACKET),
            new AmigaKeyNode("KEY_SEMICOLON","Semicolon",AM_K,0,0,(int)AmKeys.AK_SEMICOLON),
            new AmigaKeyNode("KEY_SINGLEQUOTE","Single Quote",AM_K,0,0,(int)AmKeys.AK_QUOTE),
            new AmigaKeyNode("KEY_COMMA","Comma",AM_K,0,0,(int)AmKeys.AK_COMMA),
            new AmigaKeyNode("KEY_PERIOD","Period",AM_K,0,0,(int)AmKeys.AK_PERIOD),
            new AmigaKeyNode("KEY_DIV","Slash",AM_K,0,0,(int)AmKeys.AK_SLASH),

            // new AmigaKeyNode(KEY_,"",AM_K,0,0,0x),

            /* mouse wheel "keys" */

            new AmigaKeyNode("MOUSEWHEEL_DOWN","Mouse Wheel Down",AM_K,0,0,0x7a),
            new AmigaKeyNode("MOUSEWHEEL_UP","Mouse Wheel Up",AM_K,0,0,0x7b),

            /* misc */

            new AmigaKeyNode("KEY_CDTV_STOP","CDTV Stop",AM_K,0,0,0x72),
            new AmigaKeyNode("KEY_CDTV_PLAYPAUSE","CDTV Play/Pause",AM_K,0,0,0x73),
            new AmigaKeyNode("KEY_CDTV_PREV","CDTV Previous",AM_K,0,0,0x74),
            new AmigaKeyNode("KEY_CDTV_NEXT","CDTV Next",AM_K,0,0,0x75),
            new AmigaKeyNode("KEY_CDTV_REW","CDTV Rewind",AM_K,0,0,0x76),
            new AmigaKeyNode("KEY_CDTV_FF","CDTV Fast Forward",AM_K,0,0,0x77),

            new AmigaKeyNode("KEY_0E","Keycode 0x0E",AM_K,0,0,0x0e),

            new AmigaKeyNode("KEY_1C","Keycode 0x1C",AM_K,0,0,0x1c),

            new AmigaKeyNode("KEY_2C","Keycode 0x2C",AM_K,0,0,0x2C),

            new AmigaKeyNode("KEY_3B","Keycode 0x3B",AM_K,0,0,0x3b),
            new AmigaKeyNode("KEY_47","Keycode 0x47",AM_K,0,0,0x47),	// these are not in inputevents

            new AmigaKeyNode("KEY_48","Keycode 0x48",AM_K,0,0,0x48),	//
            new AmigaKeyNode("KEY_49","Keycode 0x49",AM_K,0,0,0x49),	//
            new AmigaKeyNode("KEY_4B","Keycode 0x4B",AM_K,0,0,0x4b),	//

            new AmigaKeyNode("KEY_68","Keycode 0x68",AM_K,0,0,0x68),
            new AmigaKeyNode("KEY_69","Keycode 0x69",AM_K,0,0,0x69),
            new AmigaKeyNode("KEY_6A","Keycode 0x6A",AM_K,0,0,0x6a),
            new AmigaKeyNode("KEY_6B","Keycode 0x6B",AM_K,0,0,0x6b),	//
            new AmigaKeyNode("KEY_6C","Keycode 0x6C",AM_K,0,0,0x6c),
            new AmigaKeyNode("KEY_6D","Keycode 0x6D",AM_K,0,0,0x6d),	//
            new AmigaKeyNode("KEY_6E","Keycode 0x6E",AM_K,0,0,0x6e),	//
            new AmigaKeyNode("KEY_6F","Keycode 0x6F",AM_K,0,0,0x6f),	//
			
			new AmigaKeyNode("KEY_INSERT","Insert (PC)",AM_K,0,0,0x47),
			new AmigaKeyNode("KEY_PAGEUP","Page UP (PC)",AM_K,0,0,0x48),
			new AmigaKeyNode("KEY_PAGEDOWN","Page Down (PC)",AM_K,0,0,0x49),
			new AmigaKeyNode("KEY_F11","F11 (PC)",AM_K,0,0,0x4b),
			new AmigaKeyNode("KEY_APPS","Apps (PC)",AM_K,0,0,0x6b),
			new AmigaKeyNode("KEY_SYSRQ","PrtScr/SysRq (PC)",AM_K,0,0,0x6d),
			new AmigaKeyNode("KEY_PAUSE","Pause/Break (PC)",AM_K,0,0,0x6e),
			new AmigaKeyNode("KEY_F12","F12 (PC)",AM_K,0,0,0x6f),
			new AmigaKeyNode("KEY_HOME","Home (PC)",AM_K,0,0,0x70),
			new AmigaKeyNode("KEY_END","End (PC)",AM_K,0,0,0x71),
			
            new AmigaKeyNode("KEY_70","Keycode 0x70",AM_K,0,0,0x70),	//
            new AmigaKeyNode("KEY_71","Keycode 0x71",AM_K,0,0,0x71),	//
            new AmigaKeyNode("KEY_78","Keycode 0x78 (Reset Warning)",AM_K,0,0,0x78),
            new AmigaKeyNode("KEY_79","Keycode 0x79",AM_K,0,0,0x79),
            new AmigaKeyNode("KEY_7A","Keycode 0x7A",AM_K,0,0,0x7a),
            new AmigaKeyNode("KEY_7B","Keycode 0x7B",AM_K,0,0,0x7b),
            new AmigaKeyNode("KEY_7C","Keycode 0x7C",AM_K,0,0,0x7c),
            new AmigaKeyNode("KEY_7D","Keycode 0x7D",AM_K,0,0,0x7d),
            new AmigaKeyNode("KEY_7E","Keycode 0x7E",AM_K,0,0,0x7e),
            new AmigaKeyNode("KEY_7F","Keycode 0x7F",AM_K,0,0,0x7f),

            /* special */

			new AmigaKeyNode("SPC_START","[Miscellaneous]",AM_INFO,0,0,0),
			
			new AmigaKeyNode("SPC_CUSTOM_EVENT","<Custom Event>",AM_K,0,0,0),
            new AmigaKeyNode("SPC_ENTERGUI","Enter GUI",AM_K,0,0,(int)Aks.AKS_ENTERGUI),
            new AmigaKeyNode("SPC_SCREENSHOT","Screenshot (file)",AM_K,0,0,(int)Aks.AKS_SCREENSHOT_FILE),
			new AmigaKeyNode("SPC_SCREENSHOT_CLIPBOARD","Screenshot (clipboard)",AM_K,0,0,(int)Aks.AKS_SCREENSHOT_CLIPBOARD),
            new AmigaKeyNode("SPC_VIDEORECORD","Audio/Video recording",AM_KT,0,0,(int)Aks.AKS_VIDEORECORD),
			new AmigaKeyNode("SPC_FREEZEBUTTON","Activate Cartridge",AM_K,0,0,(int)Aks.AKS_FREEZEBUTTON),
            new AmigaKeyNode("SPC_FLOPPY0","Change disk in DF0:",AM_K,0,0,(int)Aks.AKS_FLOPPY0),
            new AmigaKeyNode("SPC_FLOPPY1","Change disk in DF1:",AM_K,0,0,(int)Aks.AKS_FLOPPY1),
            new AmigaKeyNode("SPC_FLOPPY2","Change disk in DF2:",AM_K,0,0,(int)Aks.AKS_FLOPPY2),
            new AmigaKeyNode("SPC_FLOPPY3","Change disk in DF3:",AM_K,0,0,(int)Aks.AKS_FLOPPY3),
            new AmigaKeyNode("SPC_EFLOPPY0","Eject disk in DF0:",AM_K,0,0,(int)Aks.AKS_EFLOPPY0),
            new AmigaKeyNode("SPC_EFLOPPY1","Eject disk in DF1:",AM_K,0,0,(int)Aks.AKS_EFLOPPY1),
            new AmigaKeyNode("SPC_EFLOPPY2","Eject disk in DF2:",AM_K,0,0,(int)Aks.AKS_EFLOPPY2),
            new AmigaKeyNode("SPC_EFLOPPY3","Eject disk in DF3:",AM_K,0,0,(int)Aks.AKS_EFLOPPY3),
            new AmigaKeyNode("SPC_PAUSE","Pause emulation",AM_K,0,0,(int)Aks.AKS_PAUSE),
            new AmigaKeyNode("SPC_WARP","Warp mode",AM_K,0,0,(int)Aks.AKS_WARP),
            new AmigaKeyNode("SPC_INHIBITSCREEN","Toggle screen updates",AM_K,0,0,(int)Aks.AKS_INHIBITSCREEN),
            new AmigaKeyNode("SPC_IRQ7","Level 7 interrupt",AM_K,0,0,(int)Aks.AKS_IRQ7),
            
			new AmigaKeyNode("SPC_STATEREWIND","Load previous state capture checkpoint",AM_K,0,0,(int)Aks.AKS_STATEREWIND),
            new AmigaKeyNode("SPC_STATECURRENT","Load current state capture checkpoint",AM_K,0,0,(int)Aks.AKS_STATECURRENT),
			new AmigaKeyNode("SPC_STATECAPTURE","Save state capture checkpoint",AM_K,0,0,(int)Aks.AKS_STATECAPTURE),
			
			new AmigaKeyNode("SPC_VOLUME_DOWN","Decrease volume level",AM_K,0,0,(int)Aks.AKS_VOLDOWN),
            new AmigaKeyNode("SPC_VOLUME_UP","Increase volume level",AM_K,0,0,(int)Aks.AKS_VOLUP),
            new AmigaKeyNode("SPC_VOLUME_MUTE","Mute/unmute volume",AM_K,0,0,(int)Aks.AKS_VOLMUTE),
            new AmigaKeyNode("SPC_MASTER_VOLUME_DOWN","Decrease master volume level",AM_K,0,0,(int)Aks.AKS_MVOLDOWN),
            new AmigaKeyNode("SPC_MASTER_VOLUME_UP","Increase master volume level",AM_K,0,0,(int)Aks.AKS_MVOLUP),
            new AmigaKeyNode("SPC_MASTER_VOLUME_MUTE","Mute/unmute master volume",AM_K,0,0,(int)Aks.AKS_MVOLMUTE),
            new AmigaKeyNode("SPC_QUIT","Quit emulator",AM_K,0,0,(int)Aks.AKS_QUIT),
            new AmigaKeyNode("SPC_SOFTRESET","Reset emulation",AM_K,0,0,(int)Aks.AKS_SOFTRESET),
            new AmigaKeyNode("SPC_HARDRESET","Hard reset emulation",AM_K,0,0,(int)Aks.AKS_HARDRESET),
            new AmigaKeyNode("SPC_ENTERDEBUGGER","Activate the built-in debugger",AM_K,0,0,(int)Aks.AKS_ENTERDEBUGGER),
            new AmigaKeyNode("SPC_STATESAVE","Quick save state",AM_K,0,0,(int)Aks.AKS_STATESAVEQUICK),
            new AmigaKeyNode("SPC_STATERESTORE","Quick restore state",AM_K,0,0,(int)Aks.AKS_STATERESTOREQUICK),
            new AmigaKeyNode("SPC_STATESAVEDIALOG","Save state",AM_K,0,0,(int)Aks.AKS_STATESAVEDIALOG),
            new AmigaKeyNode("SPC_STATERESTOREDIALOG","Restore state",AM_K,0,0,(int)Aks.AKS_STATERESTOREDIALOG),
			new AmigaKeyNode("SPC_TOGGLEFULLSCREEN","Toggle windowed/fullscreen",AM_K,0,0,(int)Aks.AKS_TOGGLEWINDOWEDFULLSCREEN),
			new AmigaKeyNode("SPC_TOGGLEFULLWINDOWFULLSCREEN","Toggle full-window/fullscreen",AM_K,0,0,(int)Aks.AKS_TOGGLEFULLWINDOWFULLSCREEN),
			new AmigaKeyNode("SPC_TOGGLEWINDOWFULLWINDOW","Toggle window/full-window",AM_K,0,0,(int)Aks.AKS_TOGGLEWINDOWFULLWINDOW),
			new AmigaKeyNode("SPC_TOGGLEDEFAULTSCREEN","Toggle window/default screen",AM_K,0,0,(int)Aks.AKS_TOGGLEDEFAULTSCREEN),
			new AmigaKeyNode("SPC_TOGGLEMOUSEGRAB","Toggle between mouse grabbed and un-grabbed",AM_K,0,0,(int)Aks.AKS_TOGGLEMOUSEGRAB),
			new AmigaKeyNode("SPC_DECREASE_REFRESHRATE","Decrease emulation speed",AM_K,0,0,(int)Aks.AKS_DECREASEREFRESHRATE),
			new AmigaKeyNode("SPC_INCREASE_REFRESHRATE","Increase emulation speed",AM_K,0,0,(int)Aks.AKS_INCREASEREFRESHRATE),
			new AmigaKeyNode("SPC_SWITCHINTERPOL","Switch between audio interpolation methods",AM_K,0,0,(int)Aks.AKS_SWITCHINTERPOL),
			new AmigaKeyNode("SPC_TOGGLERTG","Toggle chipset/RTG screen",AM_K,0,0,(int)Aks.AKS_TOGGLERTG),

            new AmigaKeyNode("SPC_DISKSWAPPER_NEXT","Next slot in Disk Swapper",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_NEXT),
            new AmigaKeyNode("SPC_DISKSWAPPER_PREV","Previous slot in Disk Swapper",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_PREV),
            new AmigaKeyNode("SPC_DISKSWAPPER_INSERT0","Insert disk in current Disk Swapper slot in DF0:",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_INSERT0),
            new AmigaKeyNode("SPC_DISKSWAPPER_INSERT1","Insert disk in current Disk Swapper slot in DF1:",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_INSERT1),
            new AmigaKeyNode("SPC_DISKSWAPPER_INSERT2","Insert disk in current Disk Swapper slot in DF2:",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_INSERT2),
            new AmigaKeyNode("SPC_DISKSWAPPER_INSERT3","Insert disk in current Disk Swapper slot in DF3:",AM_K,0,0,(int)Aks.AKS_DISKSWAPPER_INSERT3),

			new AmigaKeyNode("SPC_DISK_PREV0","Previous disk image in DF0:",AM_K,0,0,(int)Aks.AKS_DISK_PREV0),
			new AmigaKeyNode("SPC_DISK_PREV1","Previous disk image in DF1:",AM_K,0,0,(int)Aks.AKS_DISK_PREV1),
			new AmigaKeyNode("SPC_DISK_PREV2","Previous disk image in DF2:",AM_K,0,0,(int)Aks.AKS_DISK_PREV2),
			new AmigaKeyNode("SPC_DISK_PREV3","Previous disk image in DF3:",AM_K,0,0,(int)Aks.AKS_DISK_PREV3),
			new AmigaKeyNode("SPC_DISK_NEXT0","Next disk image in DF0:",AM_K,0,0,(int)Aks.AKS_DISK_NEXT0),
			new AmigaKeyNode("SPC_DISK_NEXT1","Next disk image in DF1:",AM_K,0,0,(int)Aks.AKS_DISK_NEXT1),
			new AmigaKeyNode("SPC_DISK_NEXT2","Next disk image in DF2:",AM_K,0,0,(int)Aks.AKS_DISK_NEXT2),
			new AmigaKeyNode("SPC_DISK_NEXT3","Next disk image in DF3:",AM_K,0,0,(int)Aks.AKS_DISK_NEXT3),
			
            new AmigaKeyNode("SPC_INPUT_CONFIG1","Select Input Configuration #1",AM_K,0,0,(int)Aks.AKS_INPUT_CONFIG_1),
            new AmigaKeyNode("SPC_INPUT_CONFIG2","Select Input Configuration #2",AM_K,0,0,(int)Aks.AKS_INPUT_CONFIG_2),
            new AmigaKeyNode("SPC_INPUT_CONFIG3","Select Input Configuration #3",AM_K,0,0,(int)Aks.AKS_INPUT_CONFIG_3),
            new AmigaKeyNode("SPC_INPUT_CONFIG4","Select Input Configuration #4",AM_K,0,0,(int)Aks.AKS_INPUT_CONFIG_4),

            new AmigaKeyNode("SPC_ARCADIA_DIAGNOSTICS","Arcadia diagnostics dip switch",AM_K,0,0,(int)Aks.AKS_ARCADIADIAGNOSTICS),
            new AmigaKeyNode("SPC_ARCADIA_PLAYER1","Arcadia player 1",AM_K,0,0,(int)Aks.AKS_ARCADIAPLY1),
            new AmigaKeyNode("SPC_ARCADIA_PLAYER2","Arcadia player 2",AM_K,0,0,(int)Aks.AKS_ARCADIAPLY2),
            new AmigaKeyNode("SPC_ARCADIA_COIN1","Arcadia coin player 1",AM_K,0,0,(int)Aks.AKS_ARCADIACOIN1),
            new AmigaKeyNode("SPC_ARCADIA_COIN2","Arcadia coin player 2",AM_K,0,0,(int)Aks.AKS_ARCADIACOIN2),
 
			new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_STOP","CDTV Front Panel Stop",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_STOP),
            new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_PLAYPAUSE","CDTV Front Panel Play/Pause",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_PLAYPAUSE),
            new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_PREV","CDTV Front Panel Previous",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_PREV),
            new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_NEXT","CDTV Front Panel Next",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_NEXT),
            new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_REW","CDTV Front Panel Rewind",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_REW),
            new AmigaKeyNode("SPC_CDTV_FRONT_PANEL_FF","CDTV Front Panel Fast Forward",AM_K,0,0,(int)Aks.AKS_CDTV_FRONT_PANEL_FF),
 
			new AmigaKeyNode("SPC_LAST","",AM_DUMMY, 0,0,0),

		};

		public static AmigaKeyDefaultNode[] AmigaDefaultKeys =
        {
            new AmigaKeyDefaultNode((int)DIKeys.DIK_ESCAPE, "KEY_ESC"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_F1, "KEY_F1"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F2, "KEY_F2"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F3, "KEY_F3"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F4, "KEY_F4"),
           
			new AmigaKeyDefaultNode((int)DIKeys.DIK_F5, "KEY_F5"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F6, "KEY_F6"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F7, "KEY_F7"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F8, "KEY_F8"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F9, "KEY_F9"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F10, "KEY_F10"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_1, "KEY_1"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_2, "KEY_2"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_3, "KEY_3"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_4, "KEY_4"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_5, "KEY_5"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_6, "KEY_6"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_7, "KEY_7"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_8, "KEY_8"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_9, "KEY_9"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_0, "KEY_0"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_TAB, "KEY_TAB"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_A, "KEY_A"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_B, "KEY_B"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_C, "KEY_C"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_D, "KEY_D"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_E, "KEY_E"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F, "KEY_F"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_G, "KEY_G"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_H, "KEY_H"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_I, "KEY_I"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_J, "KEY_J"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_K, "KEY_K"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_L, "KEY_L"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_M, "KEY_M"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_N, "KEY_N"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_O, "KEY_O"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_P, "KEY_P"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_Q, "KEY_Q"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_R, "KEY_R"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_S, "KEY_S"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_T, "KEY_T"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_U, "KEY_U"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_W, "KEY_W"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_V, "KEY_V"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_X, "KEY_X"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_Y, "KEY_Y"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_Z, "KEY_Z"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_CAPITAL, "KEY_CAPS_LOCK"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD1, "KEY_NP_1"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD2, "KEY_NP_2"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD3, "KEY_NP_3"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD4, "KEY_NP_4"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD5, "KEY_NP_5"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD6, "KEY_NP_6"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD7, "KEY_NP_7"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD8, "KEY_NP_8"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD9, "KEY_NP_9"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPAD0, "KEY_NP_0"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_DECIMAL, "KEY_NP_PERIOD"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_ADD, "KEY_NP_ADD"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_SUBTRACT, "KEY_NP_SUB"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_MULTIPLY, "KEY_NP_MUL"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_DIVIDE, "KEY_NP_DIV"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NUMPADENTER, "KEY_ENTER"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_MINUS, "KEY_SUB"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_EQUALS, "KEY_EQUALS"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_BACK, "KEY_BACKSPACE"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RETURN, "KEY_RETURN"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_SPACE, "KEY_SPACE"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_LSHIFT, "KEY_SHIFT_LEFT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_LCONTROL, "KEY_CTRL"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_LWIN, "KEY_AMIGA_LEFT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_LMENU, "KEY_ALT_LEFT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RMENU, "KEY_ALT_RIGHT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RWIN, "KEY_AMIGA_RIGHT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_APPS, "KEY_AMIGA_RIGHT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RCONTROL, "KEY_CTRL_RIGHT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RSHIFT, "KEY_SHIFT_RIGHT"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_UP, "KEY_CURSOR_UP"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_DOWN, "KEY_CURSOR_DOWN"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_LEFT, "KEY_CURSOR_LEFT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RIGHT, "KEY_CURSOR_RIGHT"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_INSERT, "KEY_AMIGA_LEFT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_DELETE, "KEY_DEL"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_HOME, "KEY_AMIGA_RIGHT"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NEXT, "KEY_HELP"),
			new AmigaKeyDefaultNode((int)DIKeys.DIK_PRIOR, "SPC_FREEZEBUTTON"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_LBRACKET, "KEY_LEFTBRACKET"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_RBRACKET, "KEY_RIGHTBRACKET"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_SEMICOLON, "KEY_SEMICOLON"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_APOSTROPHE, "KEY_SINGLEQUOTE"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_GRAVE, "KEY_BACKQUOTE"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_BACKSLASH, "KEY_BACKSLASH"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_COMMA, "KEY_COMMA"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_PERIOD, "KEY_PERIOD"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_SLASH, "KEY_DIV"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_OEM_102, "KEY_30"),
			new AmigaKeyDefaultNode((int)DIKeys.DIK_SYSRQ, "SPC_SCREENSHOT_CLIPBOARD"),
			//	{ DIK_SYSRQ, INPUTEVENT_SPC_SCREENSHOT_CLIPBOARD, 0, INPUTEVENT_SPC_SCREENSHOT, ID_FLAG_QUALIFIER_SPECIAL },

			
            new AmigaKeyDefaultNode((int)DIKeys.DIK_VOLUMEDOWN, "SPC_MASTER_VOLUME_DOWN"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_VOLUMEUP, "SPC_MASTER_VOLUME_UP"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_MUTE, "SPC_MASTER_VOLUME_MUTE"),

            new AmigaKeyDefaultNode((int)DIKeys.DIK_HOME, "KEY_70"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_END, "KEY_71"),
			new AmigaKeyDefaultNode((int)DIKeys.DIK_PAUSE, "SPC_PAUSE"),
            // new AmigaKeyDefaultNode((int)DIKeys.DIK_SYSRQ, "KEY_6E"),
            // new AmigaKeyDefaultNode((int)DIKeys.DIK_F12, "KEY_6F"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_INSERT, "KEY_47"),
            // new AmigaKeyDefaultNode((int)DIKeys.DIK_PRIOR, "KEY_48"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_PRIOR, "SPC_FREEZEBUTTON"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NEXT, "KEY_49"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_F11, "KEY_4B"),
			//	{ DIK_F12, INPUTEVENT_SPC_ENTERGUI, 0, INPUTEVENT_SPC_ENTERDEBUGGER, ID_FLAG_QUALIFIER_SPECIAL, INPUTEVENT_SPC_ENTERDEBUGGER, ID_FLAG_QUALIFIER_SHIFT, INPUTEVENT_SPC_TOGGLEDEFAULTSCREEN, ID_FLAG_QUALIFIER_CONTROL },

            new AmigaKeyDefaultNode((int)DIKeys.DIK_MEDIASTOP, "KEY_CDTV_STOP"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_PLAYPAUSE, "KEY_CDTV_PLAYPAUSE"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_PREVTRACK, "KEY_CDTV_PREV"),
            new AmigaKeyDefaultNode((int)DIKeys.DIK_NEXTTRACK, "KEY_CDTV_NEXT")
        };

		static AmigaInput()
		{
			AmigaKeyHash = new Dictionary<string, AmigaKeyNode>();

			for (int i = 0; i < AmigaKeys.Length; i++)
				AmigaKeyHash.Add(AmigaKeys[i].ConfigName, AmigaKeys[i]);
		}
	}
}
