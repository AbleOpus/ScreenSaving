# ScreenSaving
Provides functionality for developing and testing Windows screen savers.

## Screen Saver Basics
Screen savers should span across all screens on a display and the screen saver window or windows should be top-most, and do not automatically close when input is detected, this is a feature the developer must implement. Screen savers are executables that have a .scr extension. Screen saver files have context menu options that allows one to "Test", "Configure", or "Install" it.

- Test: Shows the screen saver as it would appear normally. (Note: The only time a screen saver will cause the screen to lock is when it is shown by Windows).
- Configure: Opens up a configuration for the screen saver (if the screen saver has one).
- Install: Sets the screen saver as the one to use for the current user. If the screen saver file is moved or deleted after installing it, the active screen saver will be set to none. If the screen saver has not been explicitly set to anything else and the file appears back in its original position, the active screen saver will be, once again, set to what was recently installed.

![.scr Context Menu](http://i.imgur.com/8ZX4YUj.png)

A screen saver can also be installed by dropping it in the system root, SysWOW64 directory, or the System32 directory. Example:

- C:\Windows\MyScreenSaver.scr
- C:\Windows\System32\MyScreenSaver.scr
- C:\Windows\SysWOW64\MyScreenSaver.scr

Note: 3rd Party screen savers often cannot run from System32 directory for some reason.

When Windows uses a screen saver, it passes in command line switches to specify an execution mode.
- /C (Configuration mode). Specifies the application should show settings for the screen saver.
- /P (Preview mode). Specifies that the screen saver will be shown in the tiny preview window of the screen saver settings dialog. It also specifies that the second command line argument is a handle to the preview window. An actual Windows 10 argument: "/p|3084060" (Quotes not included).

![Screen saver settings dialog](http://i.imgur.com/zWC43s0.png)

- /S (Screen saver mode). Specifies that the screen saver will be shown normally. This includes when the screen saver is shown when the user click the “Preview” button in the screen saver settings dialog. This argument is automatically passed into the executable when a .scr file is double-clicked.
- No Arguments. Use configuration mode. No arguments are passed into the screen saver when the "Configure" context menu item is clicked (for some odd reason).

## Express WinForms Implementation

1. Add a reference to the ScreenSaver library.
2. Derive your main Form from ScreenSaverForm. This Form will automatically fill the entire display. The drawing implementation will decide what elements are drawn on what screen (using the Screen class of course). This base implementation Form will also be set to be always on top, borderless, and its startup position will be manual so it can be placed over the display later on.

    ``` C#
    public partial class DebugScreenSaverForm : ScreenSaverForm
    ```

3. Paint according to whether IsPreview is set to true or false. IsPreview will be set to true if the screen saver is being placed inside the official preview window in the screen saver settings dialog.

    ``` C#
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
    
        if (IsPreview)
        {
        }
        else
        {
        }
    }
    ```
    
4. Create a settings dialog. The settings dialog must derive from Form.
5. Using Visual Studio's built in settings will be unpredictable at best with screen savers, so use the SettingsBase class provided by the ScreenSaver library (more on this later).
6. In the Program class, add the following using directives:

    ``` C#
    using ScreenSaving.Forms;
    using ScreenSaving.ScreenSavers;
    ```
    
7. Remove the Application.Run() line from the program class and add this:

    ``` C#
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        WinFormsScreenSaver<DebugScreenSaverForm, SettingsDialog>.Run(args);
    }
    ```
    
This states that we are running a WindowsForms-based screen saver. The first type parameter is the actual screen saver form type. This abstraction assumes you are covering the entire display with one Form. The second type parameter is the Form type defined for the screen saver's settings dialog. A NoSettingsForm type is provided to specify that no settings are available. Use this where SettingsDialog is in the above code to provide a simple "no settings" notification to the user when the user tries to access the settings. Finally, we pass our command line arguments into Run() and the abstraction does the rest.
8. Install Fody.Costura. Nuget command: "Install-Package Costura.Fody". This will merge all libraries into the application assembly. This is especially important if an installer is not used to install and uninstall the screen saver.
9. Under the screen saver project properties, under "Build Events", add the following commands into the post build commands textbox:

    ``` CMD
    REM Change the filenames here when the assembly name changes
    copy $(TargetFileName) $(TargetName).scr
    REM Unable to properly use Fody cleanup functionality, so do this....
    DEL *.dll *.xml
    ```

The first line copies the output executable to the same output directory just with the .scr extension. The second line gets rid of the unneeded DLL and .XML files (assuming you are using Costura). The .exe is not needed in the release but it is not be deleted post build as it is what Visual Studio executes and attaches the debugger to. 

## Using SettingsBase

The SettingsBase class uses a singleton much like ApplicationSettingsBase. It saves to %appdata% in a familiar way as well. A directory having the "product name" of the application is created in the roaming or local directory. Placed within that directory is .dat files, holding settings for the application. A file will exist for each major revision of the application. So version 3.0.0.0 will have different settings from 2.0.0.0.

In the screen saver project, derive from SettingsBase and mark the class as Serializable. Create a property for each setting, they will be automatically serialized and deserialized. Finally, implement the Reset() method. The base class uses BinaryFormatter for serialization, so it can serialize just about anything.

``` C#
[Serializable]
class Settings : SettingsBase<Settings>
{
    public Color ForeColor { get; set; }
    public bool ShowScreenID { get; set; }
    public bool ShowScreenBounds { get; set; }
    
    public override void Reset()
    {
        ForeColor = Color.YellowGreen;
        ShowScreenID = true;
        ShowScreenBounds = true;
    }
}
```

## Advanced Screen Saver Implementations
The ScreenSaving.ScreenSavers namespace provides the main abstraction for the library. For more flexibility, derive from the StandardScreenSaver class (in opposed to using the WPF or WinForms abstractions). The StandardScreenSaver class provides built-in functionality for detecting global input and will hide the cursor when the screen saver is shown. The protected member ActivityDetector, has a property to set how sensitive the screen saver is to mouse movements. If this value is set to 100, then the mouse must move a 100 pixel distance in less than 1 second for the OnActivityDetected method to raise.

Implement the OnShowScreenSaver method. Herein, the screen saver is to be shown normally. The following code sets the bounds of a globally declared Form. The bounds are set to the provided display bounds (which entails all of the screens). The code then shows the Form with Application.Run().

``` C#
protected override void OnShowScreenSaver(Rectangle displayBounds)
{
    base.OnShowScreenSaver(displayBounds);
    formScreenSaver.Bounds = displayBounds;
    Application.Run(formScreenSaver);
}
```

Implement the OnShowConfiguration method. The following code shows a user-defined settings Form:

``` C#
protected override void OnShowConfiguration()
{
    using (var dialogSettings = new MySettingsForm())
    {
           dialogSettings.ShowDialog()
    }
}
```

Implement the OnShowPreview method. This method is provides the preview window, so one can easily show the screen saver in it. Simple assign your screen saver window’s handle to the handle of the preview window. Then show the screen saver window.

``` C#
protected override void OnShowPreview(PreviewWindow previewWindow)
{
    previewWindow.Child = formScreenSaver.Handle;
    Application.Run(formScreenSaver);
} 
```

Implement the OnActivityDetected method. Typically you would close the screen saver immediately, however, it is possible to start a fade out animation here, then close the application when the fade is completed. Be sure to call Dispose() when done with the screen saver. This is important considering the StandardScreenSaver installs global hooks which need to be unhooked as soon as possible.

``` C#
protected override void OnActivityDetected()
{
    Dispose();
    Application.Exit();
}
```

## Debugging Features
The solution has many configurations. All debug configurations output to the debug directory (in opposed to having their own output location). The solution and project configurations are defined so the demo applications can be started in different modes. The project configurations determine what arguments will be passed into the executable when it is debugged. The following are the debug solution configurations currently available:  

- DebugScreenSaver: Starts the application in screen saver mode (/s).
- DebugConfig: Starts the application in configuration mode (/c).
- DebugPreview: Starts the application in preview mode (/p). The preview will be shown in a test window just like it is shown in the Windows screen saver settings dialog.

![Activity Test Form](http://i.imgur.com/2eOGmgo.png)

- DebugActivity: Starts a Form for testing mouse move threshold values (how far the mouse has to move in 1 second to register as activity). This mode is specific to the WinFormsDemo and will likely never be implemented in WPF.

![Activity Test Form](http://i.imgur.com/9Z4ij3M.png)

Any Form that is derived from ScreenSaverForm will have its TopMost value automatically set to false while in debug mode, otherwise it will be set to true.

## Issues
- I am not proficient enough with WPF to implement a WPF demo. The WPF demo does not work beyond screen saver mode.
