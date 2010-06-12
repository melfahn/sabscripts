//
//  Copyright (C) 2009 Team Blackbolt
//  http://www.teamblackbolt.co.uk/
//
//  This Program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2, or (at your option)
//  any later version.
//
//  This Program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace XBMC
{
    public enum Buttons
    {
        power,
        mytv,
        mymusic,
        mypictures,
        myvideo,
        stop,
        record,
        pause,
        play,
        reverse,
        forward,
        skipminus,
        skipplus,
        up,
        back,
        info,
        select,
        right,
        left,
        down,
        volumeplus,
        pageplus,
        start,
        volumeminus,
        pageminus,
        mute,
        recordtv,
        menu,
        title,
        livetv,
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        star,
        zero,
        hash,
        clear,
        display, 
        ActionButton
    }

    public enum IconType
    {
        ICON_NONE = 0x00,
        ICON_JPEG = 0x01,
        ICON_PNG = 0x02,
        ICON_GIF = 0x03
    }

    public enum ButtonFlagsType
    {
        BTN_USE_NAME = 0x01,
        BTN_DOWN = 0x02,
        BTN_UP = 0x04,
        BTN_USE_AMOUNT = 0x08,
        BTN_QUEUE = 0x10,
        BTN_NO_REPEAT = 0x20,
        BTN_VKEY = 0x40,
        BTN_AXIS = 0x80
    }

    public enum MouseFlagsType
    {
        MS_ABSOLUTE = 0x01
    }

    public enum LogTypeEnum
    {
        LOGDEBUG = 0,
        LOGINFO = 1,
        LOGNOTICE = 2,
        LOGWARNING = 3,
        LOGERROR = 4,
        LOGSEVERE = 5,
        LOGFATAL = 6,
        LOGNONE = 7
    }
    public enum BuiltInActions
    {
        Help, //This help message
        Reboot, //Reboot the xbox (power cycle)
        Restart, //Restart the xbox (power cycle)
        ShutDown, //Shutdown the xbox
        Powerdown, //Powerdown system
        Quit, //Quit XBMC
        Hibernate, //Hibernates the system
        Suspend, //Suspends the system
        RestartApp, //Restart XBMC
        Minimize, //Minimize XBMC
        Credits, //Run XBMCs Credits
        Reset, //Reset the xbox (warm reboot)
        Mastermode, //Control master mode
        ActivateWindow, //Activate the specified window
        ReplaceWindow, //Replaces the current window with the new one
        TakeScreenshot, //Takes a Screenshot
        RunScript, //Run the specified script
        RunAppleScript, //Run the specified AppleScript command
        RunPlugin, //Run the specified plugin
        Extract, //Extracts the specified archive
        PlayMedia, //Play the specified media file (or playlist)
        SlideShow, //Run a slideshow from the specified directory
        RecursiveSlideShow, //Run a slideshow from the specified directory, including all subdirs
        ReloadSkin, //Reload XBMC's skin
        RefreshRSS, //Reload RSS feeds from RSSFeeds.xml"},
        PlayerControl, //Control the music or video player
        Playlist_PlayOffset, //Start playing from a particular offset in the playlist
        Playlist_Clear, //Clear the current playlist
        EjectTray, //Close or open the DVD tray
        AlarmClock, //Prompt for a length of time and start an alarm clock
        CancelAlarm, //Cancels an alarm
        Action, //Executes an action for the active window (same as in keymap)
        Notification, //Shows a notification on screen, specify header, then message, and optionally time in milliseconds and a icon.
        PlayDVD, //Plays the inserted CD or DVD media from the DVD-ROM Drive!
        Skin_ToggleSetting, //Toggles a skin setting on or off
        Skin_SetString, //Prompts and sets skin string
        Skin_SetNumeric, //Prompts and sets numeric input
        Skin_SetPath, //Prompts and sets a skin path
        Skin_Theme, //Control skin theme
        Skin_SetImage, //Prompts and sets a skin image
        Skin_SetLargeImage, //Prompts and sets a large skin images
        Skin_SetFile, //Prompts and sets a file
        Skin_SetBool, //Sets a skin setting on
        Skin_Reset, //Resets a skin setting to default
        Skin_ResetSettings, //Resets all skin settings
        Mute, //Mute the player
        SetVolume, //Set the current volume
        Dialog_Close, //Close a dialog
        System_LogOff, //Log off current user
        System_Exec, //Execute shell commands
        System_ExecWait, //Execute shell commands and freezes XBMC until shell is closed
        Resolution, //Change XBMC's Resolution
        SetFocus, //Change current focus to a different control id
        UpdateLibrary, //Update the selected library (music or video)
        CleanLibrary, //Clean the video library
        PageDown, //Send a page down event to the pagecontrol with given id
        PageUp, //Send a page up event to the pagecontrol with given id
        LastFM_Love, //Add the current playing last.fm radio track to the last.fm loved tracks
        LastFM_Ban, //Ban the current playing last.fm radio track
        Container_Refresh, //Refresh current listing
        Container_Update, //Update current listing. Send Container.Update(path,replace) to reset the path history
        Container_NextViewMode, //Move to the next view type (and refresh the listing)
        Container_PreviousViewMode, //Move to the previous view type (and refresh the listing)
        Container_SetViewMode, //Move to the view with the given id
        Container_NextSortMethod, //Change to the next sort method
        Container_PreviousSortMethod, //Change to the previous sort method
        Container_SetSortMethod, //Change to the specified sort method
        Container_SortDirection, //Toggle the sort direction
        Control_Move, //Tells the specified control to 'move' to another entry specified by offset
        Control_SetFocus, //Change current focus to a different control id
        Control_Message, //Send a given message to a control within a given window
        SendClick, //Send a click message from the given control to the given window
        LoadProfile, //Load the specified profile (note; if locks are active it won't work)
        SetProperty, //Sets a window property for the current window (key,value)
        PlayWith, //Play the selected item with the specified core
        LIRC_Stop, //Removes XBMC as LIRC client
        LIRC_Start, //Adds XBMC as LIRC client
        LCD_Suspend, //Suspends LCDproc
        LCD_Resume //Resumes LCDproc
    }
    public enum ActionType
    {
        ACTION_EXECBUILTIN = 0x01,
        ACTION_BUTTON = 0x02
    }

    public class EventClient
    {
        static object _lock = new object();
        static EventClient _Current;
        public static EventClient Current
        {
            get
            {
                lock (_lock)
                {
                    if (_Current == null)
                    {
                        _Current = new EventClient();                        
                    }
                }
                return _Current;
            }
            set
            {
                _Current = value;
            }
        }

        /************************************************************************/
        /* Written by Peter Tribe aka EqUiNox (TeamBlackbolt)                   */
        /* Based upon XBMC's xbmcclient.cpp class                               */
        /************************************************************************/

        private enum PacketType
        {
            PT_HELO = 0x01,
            PT_BYE = 0x02,
            PT_BUTTON = 0x03,
            PT_MOUSE = 0x04,
            PT_PING = 0x05,
            PT_BROADCAST = 0x06,  //Currently not implemented
            PT_NOTIFICATION = 0x07,
            PT_BLOB = 0x08,
            PT_LOG = 0x09,
            PT_ACTION = 0x0A,
            PT_DEBUG = 0xFF //Currently not implemented
        }

        private const int STD_PORT = 9777;
        private const int MAX_PACKET_SIZE = 1024;
        private const int HEADER_SIZE = 32;
        private const int MAX_PAYLOAD_SIZE = MAX_PACKET_SIZE - HEADER_SIZE;
        private const byte MAJOR_VERSION = 2;
        private const byte MINOR_VERSION = 0;

        private uint uniqueToken;
        private Socket socket;

        public bool Connect(string Address)
        {
            return Connect(Address, STD_PORT, (uint)System.DateTime.Now.TimeOfDay.Milliseconds);
        }

        public bool Connect(string Address, int Port)
        {
            return Connect(Address, Port, (uint)System.DateTime.Now.TimeOfDay.Milliseconds);
        }


        public bool Connect(string Address, int Port, uint UID)
        {
            try
            {

                if (socket != null) Disconnect();
                uniqueToken = UID;
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress ip;
                if (!IPAddress.TryParse(Address, out ip))
                {
                    IPHostEntry ipHostEntry = Dns.GetHostEntry(Address);
                    foreach (IPAddress ipAddress in ipHostEntry.AddressList)
                    {
                        if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ip = ipAddress;
                            break;
                        }
                    }
                }
                socket.Connect(new System.Net.IPEndPoint(ip, Port));
                return socket.Connected;

            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
                return false;
            }
        }

        public bool Connected
        {
            get {
                if (socket == null) return false;
                return socket.Connected; 
            }
        }

        public void Disconnect()
        {
            try
            {
                if (socket != null)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket = null;
                }
            }
            catch
            {
            }
        }

        private byte[] Header(PacketType PacketType, int NumberOfPackets, int CurrentPacket, int PayloadSize)
        {

            byte[] header = new byte[HEADER_SIZE];

            header[0] = (byte)'X';
            header[1] = (byte)'B';
            header[2] = (byte)'M';
            header[3] = (byte)'C';

            header[4] = MAJOR_VERSION;
            header[5] = MINOR_VERSION;

            if (CurrentPacket == 1)
            {
                header[6] = (byte)(((ushort)PacketType & 0xff00) >> 8);
                header[7] = (byte)((ushort)PacketType & 0x00ff);
            }
            else
            {
                header[6] = (byte)(((ushort)PacketType.PT_BLOB & 0xff00) >> 8);
                header[7] = (byte)((ushort)PacketType.PT_BLOB & 0x00ff);
            }

            header[8] = (byte)((CurrentPacket & 0xff000000) >> 24);
            header[9] = (byte)((CurrentPacket & 0x00ff0000) >> 16);
            header[10] = (byte)((CurrentPacket & 0x0000ff00) >> 8);
            header[11] = (byte)(CurrentPacket & 0x000000ff);

            header[12] = (byte)((NumberOfPackets & 0xff000000) >> 24);
            header[13] = (byte)((NumberOfPackets & 0x00ff0000) >> 16);
            header[14] = (byte)((NumberOfPackets & 0x0000ff00) >> 8);
            header[15] = (byte)(NumberOfPackets & 0x000000ff);

            header[16] = (byte)((PayloadSize & 0xff00) >> 8);
            header[17] = (byte)(PayloadSize & 0x00ff);

            header[18] = (byte)((uniqueToken & 0xff000000) >> 24);
            header[19] = (byte)((uniqueToken & 0x00ff0000) >> 16);
            header[20] = (byte)((uniqueToken & 0x0000ff00) >> 8);
            header[21] = (byte)(uniqueToken & 0x000000ff);

            return header;

        }

        private bool Send(PacketType PacketType, byte[] Payload)
        {
            try
            {

                bool successfull = true;
                int packetCount = (Payload.Length / MAX_PAYLOAD_SIZE) + 1;
                int bytesToSend = 0;
                int bytesSent = 0;
                int bytesLeft = Payload.Length;

                for (int Package = 1; Package <= packetCount; Package++)
                {

                    if (bytesLeft > MAX_PAYLOAD_SIZE)
                    {
                        bytesToSend = MAX_PAYLOAD_SIZE;
                        bytesLeft -= bytesToSend;
                    }
                    else
                    {
                        bytesToSend = bytesLeft;
                        bytesLeft = 0;
                    }

                    byte[] header = Header(PacketType, packetCount, Package, bytesToSend);
                    byte[] packet = new byte[MAX_PACKET_SIZE];

                    Array.Copy(header, 0, packet, 0, header.Length);
                    Array.Copy(Payload, bytesSent, packet, header.Length, bytesToSend);

                    int sendSize = socket.Send(packet, header.Length + bytesToSend, SocketFlags.None);

                    if (sendSize != (header.Length + bytesToSend))
                    {
                        successfull = false;
                        break;
                    }

                    bytesSent += bytesToSend;

                }

                return successfull;

            }
            catch
            {

                return false;

            }

        }

        public bool SendHelo(string DevName, string IconResourceName, IconType IconType)
        {
            byte[] icon = Resources.ResourceManager.GetRawLogo(IconResourceName);
            if (IconType != IconType.ICON_NONE && icon!=null && icon.Length>0)
            {
                return SendHelo(DevName, IconType, icon);
            }
            return SendHelo(DevName);
        }

        public bool SendHelo(string DevName, IconType IconType, string IconFile)
        {

            byte[] icon = new byte[0];
            if (IconType != IconType.ICON_NONE)
            {
                using (System.IO.FileStream stm = File.Open(IconFile, FileMode.Open))
                {
                    stm.Read(icon, 0, (int)stm.Length);
                }
            }
            return SendHelo(DevName, IconType, icon);
        }
        /************************************************************************/
        /* SendHelo - Payload format                                            */
        /* %s -  device name (max 128 chars)                                    */
        /* %c -  icontype ( 0=>NOICON, 1=>JPEG , 2=>PNG , 3=>GIF )              */
        /* %s -  my port ( 0=>not listening )                                   */
        /* %d -  reserved1 ( 0 )                                                */
        /* %d -  reserved2 ( 0 )                                                */
        /* XX -  imagedata ( can span multiple packets )                        */
        /************************************************************************/
        public bool SendHelo(string DevName, IconType IconType, byte[] icon)
        {


            byte[] payload = new byte[DevName.Length + 12 + icon.Length];

            int offset = 0;

            for (int i = 0; i < DevName.Length; i++)
                payload[offset++] = (byte)DevName[i];
            payload[offset++] = (byte)'\0';

            payload[offset++] = (byte)IconType;

            payload[offset++] = (byte)0;
            payload[offset++] = (byte)'\0';

            for (int i = 0; i < 8; i++)
                payload[offset++] = (byte)0;

            Array.Copy(icon, 0, payload, DevName.Length + 12, icon.Length);

            return Send(PacketType.PT_HELO, payload);

        }

        public bool SendHelo(string DevName)
        {
            return SendHelo(DevName, IconType.ICON_NONE, "");
        }

        /************************************************************************/
        /* SendNotification - Payload format                                    */
        /* %s - caption                                                         */
        /* %s - message                                                         */
        /* %c - icontype ( 0=>NOICON, 1=>JPEG , 2=>PNG , 3=>GIF )               */
        /* %d - reserved ( 0 )                                                  */
        /* XX - imagedata ( can span multiple packets )                         */
        /************************************************************************/
        public bool SendNotification(string Caption, string Message, IconType IconType, string IconFile)
        {

            byte[] icon = new byte[0];
            if (IconType != IconType.ICON_NONE)
            {

                icon = Resources.ResourceManager.GetRawLogo(IconFile);
            }

            byte[] payload = new byte[Caption.Length + Message.Length + 7 + icon.Length];

            int offset = 0;

            for (int i = 0; i < Caption.Length; i++)
                payload[offset++] = (byte)Caption[i];
            payload[offset++] = (byte)'\0';

            for (int i = 0; i < Message.Length; i++)
                payload[offset++] = (byte)Message[i];
            payload[offset++] = (byte)'\0';

            payload[offset++] = (byte)IconType;

            for (int i = 0; i < 4; i++)
                payload[offset++] = (byte)0;

            Array.Copy(icon, 0, payload, Caption.Length + Message.Length + 7, icon.Length);

            return Send(PacketType.PT_NOTIFICATION, payload);

        }

        public bool SendNotification(string Caption, string Message)
        {
            return SendNotification(Caption, Message, IconType.ICON_NONE, "");
        }

        /************************************************************************/
        /* SendButton - Payload format                                          */
        /* %i - button code                                                     */
        /* %i - flags 0x01 => use button map/name instead of code               */
        /*            0x02 => btn down                                          */
        /*            0x04 => btn up                                            */
        /*            0x08 => use amount                                        */
        /*            0x10 => queue event                                       */
        /*            0x20 => do not repeat                                     */
        /*            0x40 => virtual key                                       */
        /*            0x80 => axis key                                          */
        /* %i - amount ( 0 => 65k maps to -1 => 1 )                             */
        /* %s - device map (case sensitive and required if flags & 0x01)        */
        /*      "KB" - Standard keyboard map                                    */
        /*      "XG" - Xbox Gamepad                                             */
        /*      "R1" - Xbox Remote                                              */
        /*      "R2" - Xbox Universal Remote                                    */
        /*      "LI:devicename" -  valid LIRC device map where 'devicename'     */
        /*                         is the actual name of the LIRC device        */
        /*      "JS<num>:joyname" -  valid Joystick device map where            */
        /*                           'joyname'  is the name specified in        */
        /*                           the keymap. JS only supports button code   */
        /*                           and not button name currently (!0x01).     */
        /* %s - button name (required if flags & 0x01)                          */
        /************************************************************************/
        private bool SendButton(string Button, ushort ButtonCode, string DeviceMap, ButtonFlagsType Flags, short Amount)
        {

            if (Button.Length != 0)
            {
                if ((Flags & ButtonFlagsType.BTN_USE_NAME) == 0)
                    Flags |= ButtonFlagsType.BTN_USE_NAME;
                ButtonCode = 0;
            }
            else
                Button = "";

            if (Amount > 0)
            {
                if ((Flags & ButtonFlagsType.BTN_USE_AMOUNT) == 0)
                    Flags |= ButtonFlagsType.BTN_USE_AMOUNT;
            }

            if ((Flags & ButtonFlagsType.BTN_DOWN) == 0 || (Flags & ButtonFlagsType.BTN_UP) == 0)
                Flags |= ButtonFlagsType.BTN_DOWN;

            byte[] payload = new byte[Button.Length + DeviceMap.Length + 8];

            int offset = 0;

            payload[offset++] = (byte)((ButtonCode & 0xff00) >> 8);
            payload[offset++] = (byte)(ButtonCode & 0x00ff);

            payload[offset++] = (byte)(((ushort)Flags & 0xff00) >> 8);
            payload[offset++] = (byte)((ushort)Flags & 0x00ff);

            payload[offset++] = (byte)((Amount & 0xff00) >> 8);
            payload[offset++] = (byte)(Amount & 0x00ff);

            for (int i = 0; i < DeviceMap.Length; i++)
                payload[offset++] = (byte)DeviceMap[i];
            payload[offset++] = (byte)'\0';

            for (int i = 0; i < Button.Length; i++)
                payload[offset++] = (byte)Button[i];
            payload[offset++] = (byte)'\0';

            return Send(PacketType.PT_BUTTON, payload);

        }

        public bool SendSingleButton(Buttons Button)
        {
            bool a = this.SendButton(Button.ToString(), "R1", XBMC.ButtonFlagsType.BTN_DOWN, 0);
            if (a)
            {
                System.Threading.Thread.Sleep(100);
                a = this.SendButton();
            }
            return a;
        }
        public bool SendButton(Buttons Button)
        {
            return SendButton(Button, "R1", ButtonFlagsType.BTN_DOWN, 0);
        }
        public bool SendButton(Buttons Button, string DeviceMap, ButtonFlagsType Flags, short Amount)
        {
            return SendButton(Button.ToString(), 0, DeviceMap, Flags, Amount);
        }
        public bool SendButton(string Button, string DeviceMap, ButtonFlagsType Flags, short Amount)
        {
            return SendButton(Button, 0, DeviceMap, Flags, Amount);
        }

        public bool SendButton(string Button, string DeviceMap, ButtonFlagsType Flags)
        {
            return SendButton(Button, 0, DeviceMap, Flags, 0);
        }

        public bool SendButton(ushort ButtonCode, string DeviceMap, ButtonFlagsType Flags, short Amount)
        {
            return SendButton("", ButtonCode, DeviceMap, Flags, Amount);
        }

        public bool SendButton(ushort ButtonCode, string DeviceMap, ButtonFlagsType Flags)
        {
            return SendButton("", ButtonCode, DeviceMap, Flags, 0);
        }

        public bool SendButton(ushort ButtonCode, ButtonFlagsType Flags, short Amount)
        {
            return SendButton("", ButtonCode, "", Flags, Amount);
        }

        public bool SendButton(ushort ButtonCode, ButtonFlagsType Flags)
        {
            return SendButton("", ButtonCode, "", Flags, 0);
        }

        public bool SendButton()
        {
            return SendButton("", 0, "", ButtonFlagsType.BTN_UP, 0);
        }

        /************************************************************************/
        /* SendPing - No payload                                                */
        /************************************************************************/
        public bool SendPing()
        {
            byte[] payload = new byte[0];
            return Send(PacketType.PT_PING, payload);
        }

        /************************************************************************/
        /* SendBye - No payload                                                 */
        /************************************************************************/
        public bool SendBye()
        {
            byte[] payload = new byte[0];
            return Send(PacketType.PT_BYE, payload);
        }

        /************************************************************************/
        /* SendMouse - Payload format                                           */
        /* %c - flags                                                           */
        /*    - 0x01 absolute position                                          */
        /* %i - mousex (0-65535 => maps to screen width)                        */
        /* %i - mousey (0-65535 => maps to screen height)                       */
        /************************************************************************/
        public bool SendMouse(ushort X, ushort Y, MouseFlagsType Flags)
        {

            byte[] payload = new byte[9];

            int offset = 0;

            payload[offset++] = (byte)Flags;

            payload[offset++] = (byte)((X & 0xff00) >> 8);
            payload[offset++] = (byte)(X & 0x00ff);

            payload[offset++] = (byte)((Y & 0xff00) >> 8);
            payload[offset++] = (byte)(Y & 0x00ff);

            return Send(PacketType.PT_MOUSE, payload);

        }

        public bool SendMouse(ushort X, ushort Y)
        {
            return SendMouse(X, Y, MouseFlagsType.MS_ABSOLUTE);
        }

        /************************************************************************/
        /* SendLog - Payload format                                             */
        /* %c - log type                                                        */
        /* %s - message                                                         */
        /************************************************************************/
        public bool SendLog(LogTypeEnum LogLevel, string Message)
        {

            byte[] payload = new byte[Message.Length + 2];

            int offset = 0;

            payload[offset++] = (byte)LogLevel;

            for (int i = 0; i < Message.Length; i++)
                payload[offset++] = (byte)Message[i];
            payload[offset++] = (byte)'\0';

            return Send(PacketType.PT_LOG, payload);

        }

        /************************************************************************/
        /* SendAction - Payload format                                          */
        /* %c - action type                                                     */
        /* %s - action message                                                  */
        /************************************************************************/
        public bool SendAction(ActionType Action, string Message)
        {

            byte[] payload = new byte[Message.Length + 2];

            int offset = 0;

            payload[offset++] = (byte)Action;

            for (int i = 0; i < Message.Length; i++)
                payload[offset++] = (byte)Message[i];
            payload[offset++] = (byte)'\0';

            
            
            return Send(PacketType.PT_ACTION, payload);

        
       
        }
        //http://xbmc.org/wiki/?title=List_of_Built_In_Functions
        public bool SendAction(string Message, string Params)
        {
            return SendAction(ActionType.ACTION_EXECBUILTIN, string.Format("{0} {1}", Message, Params).Trim());
        }

    }
}
