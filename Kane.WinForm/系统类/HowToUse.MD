### How To Use It.
``` C#
	public void HowToUse()
	{
	    WinHotKey hotkey = new WinHotKey(this.Handle);
	    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D6, "����һ");
	    hotkey.RegisterHotkey(KeyFlags.Ctrl | KeyFlags.Shift, Keys.D7, "���Զ�");
	    hotkey.HotKeyAction += (hotKeyID) =>
	    {
	        if (hotKeyID == hotkey.GetHotKeyID("����һ"))
	        {
	            //Do something
	        }
	        if (hotKeyID == hotkey.GetHotKeyID("���Զ�"))
	        {
	            //Do something
	        }
	    };
	} 
```