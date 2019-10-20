### How To Use It.
``` C#
	public void HowToUse()
	{
	    WinHotKey hotkey = new WinHotKey(this.Handle);
	    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D6, "≤‚ ‘“ª");
	    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D7, "≤‚ ‘∂˛");
	    hotkey.HotKeyAction += (hotKeyID) =>
	    {
	        if (hotKeyID == hotkey.GetHotKeyID("≤‚ ‘“ª"))
	        {
	            //Do something
	        }
	        if (hotKeyID == hotkey.GetHotKeyID("≤‚ ‘∂˛"))
	        {
	            //Do something
	        }
	    };
	} 
```