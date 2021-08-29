#define MyAppName "B&W2 - Fan Patch"
#define MyAppVersion "1.42"
#define MyAppPublisher "Egerion"
#define MyAppURL "http://www.bwrealm.com/ and http://www.bwgame.net"
#define MyAppExeName "B&W2 Fan Patch v1.42 Installer.exe"

[Setup]
//kSign /d $qYOUR_DESCRIPTION$q /du $qhttp://www.example.com$q $f
//SignTool=signtool
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
OutputBaseFilename= {#MyAppName}
WizardResizable=no
WizardStyle=modern 
DefaultDirName={sd}
UsePreviousAppDir=no
Uninstallable=no
AllowRootDirectory=yes
AppendDefaultDirName=no
AllowNetworkDrive=yes
AllowUNCPath=yes
DisableReadyPage=yes
DisableDirPage=yes
Compression=lzma2
SolidCompression=yes

;wizard image files 
SetupIconFile="C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchIcon.ico"
WizardSmallImageFile="C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\WizModernSmallImage.bmp"
WizardImageFile="C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\WizModernImage.bmp"

LicenseFile = "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Licenses\Fan Patch v1.42 License.txt"

;output location 
OutputDir= "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Out"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
;readme
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Fan Patch v1.42\readme_v1_42.txt"; DestDir: "{code:GetDestDir}"; Flags: isreadme;

;cover image
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchLogo1.bmp"; DestDir: "{tmp}";
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchLogo2.bmp"; DestDir: "{tmp}";
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchLogo3.bmp"; DestDir: "{tmp}";
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchLogo4.bmp"; DestDir: "{tmp}";
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Setup_Images\Fan Patch\FanPatchLogo5.bmp"; DestDir: "{tmp}";

;common files 
Source: "C:\Users\Ege\Desktop\Tribal Pack - Compiler\Fan Patch v1.42\*";  DestDir: "{code:GetDestDir}"; Flags: ignoreversion recursesubdirs;

[Code]
var

  WelcomePageID:  Integer;
  InstallPath:    string;
  StartTick:      DWORD;

  //images
  FanPatchCover:     TBitmapImage;

  BackgroundImage:  TBitmapImage;

  //pages
  GameVersionPage:  TWizardPage;
  TribeTypePage:    TWizardPage;
  TownCenterPage:   TWizardPage;
  BeforeBeginPage:  TWizardPage;
  InstallDirPage:   TInputDirWizardPage;

  //labels
  DataDirExampleLabel:  TNewStaticText;
  PercentLabel:         TNewStaticText;
  ElapsedLabel:         TNewStaticText;
  RemainingLabel:       TNewStaticText;

//percentage bar 
function GetTickCount: DWORD;
  external 'GetTickCount@kernel32.dll stdcall';


function TicksToStr(Value: DWORD): string;
var
  I: DWORD;
  Hours, Minutes, Seconds: Integer;
begin
  I := Value div 1000;
  Seconds := I mod 60;
  I := I div 60;
  Minutes := I mod 60;
  I := I div 60;
  Hours := I mod 24;
  Result := Format('%.2d:%.2d:%.2d', [Hours, Minutes, Seconds]);
end;

procedure InitializeWizard;
var
  WelcomePage: TWizardPage;  
  WebsiteButton: TButton;

  BitmapFileName: string;

  RandomNumber: integer;
begin

  PercentLabel := TNewStaticText.Create(WizardForm);
  ElapsedLabel := TNewStaticText.Create(WizardForm);
  RemainingLabel := TNewStaticText.Create(WizardForm);

  with PercentLabel do 
  begin
    Parent := WizardForm.ProgressGauge.Parent;
    Left := 0;
    Top := WizardForm.ProgressGauge.Top +
    WizardForm.ProgressGauge.Height + 12;
  end;

  with ElapsedLabel do 
  begin
    Parent := WizardForm.ProgressGauge.Parent;
    Left := 0;
    Top := PercentLabel.Top + PercentLabel.Height + 4;
  end;

  with RemainingLabel do
  begin   
    Parent := WizardForm.ProgressGauge.Parent;
    Left := 0;
    Top := ElapsedLabel.Top + ElapsedLabel.Height + 4;
  end;

  //install directory page 
  InstallDirPage := CreateInputDirPage(wpReady,'Select Your Game Folder', '','You need to locate Black and White 2 folder to proceed.',False, '');
  InstallDirPage.Add('Select the "Black and White 2" folder to install, then click next.');

  //welcome page 
  WelcomePage := CreateCustomPage(wpWelcome, '', '');
  WelcomePageID := WelcomePage.ID;

  RandomNumber := Random(5);
  FanPatchCover := TBitmapImage.Create(WizardForm);
  if RandomNumber = 0 then BitmapFileName := ExpandConstant('{tmp}\FanPatchLogo1.bmp');
  if RandomNumber = 1 then BitmapFileName := ExpandConstant('{tmp}\FanPatchLogo2.bmp');
  if RandomNumber = 2 then BitmapFileName := ExpandConstant('{tmp}\FanPatchLogo3.bmp');
  if RandomNumber = 3 then BitmapFileName := ExpandConstant('{tmp}\FanPatchLogo4.bmp');
  if RandomNumber = 4 then BitmapFileName := ExpandConstant('{tmp}\FanPatchLogo5.bmp');

  ExtractTemporaryFile(ExtractFileName(BitmapFileName));

  with FanPatchCover do
  begin
    Parent := WizardForm.InnerPage;

    AutoSize := True;
    stretch := True;
    
    Align := alClient; //auto scale 

    Bitmap.LoadFromFile(BitmapFileName);
  end;

  //hide extract file names
  begin
    WizardForm.ProgressGauge.Visible := True;
    WizardForm.StatusLabel.Visible := False;
    WizardForm.FilenameLabel.Visible := False;
    WizardForm.TypesCombo.Visible := False;
    WizardForm.IncTopDecHeight(WizardForm.ComponentsList,
    -(WizardForm.ComponentsList.Top - WizardForm.TypesCombo.Top));
  end;

end;

procedure CurPageChanged(CurPageID: Integer);
begin

  FanPatchCover.Visible := CurPageID = WelcomePageID;
  WizardForm.Bevel1.Visible := CurPageID <> WelcomePageID;
  WizardForm.MainPanel.Visible := CurPageID <> WelcomePageID;
  WizardForm.InnerNotebook.Visible := CurPageID <> WelcomePageID;

  //percentage bar 
  if CurPageID = wpInstalling then
  begin
    StartTick := GetTickCount;
  end;

  //wpWelcome, wpLicense, wpPassword, wpInfoBefore, wpUserInfo, wpSelectDir, wpSelectComponents, wpSelectProgramGroup, wpSelectTasks, wpReady, wpPreparing, wpInstalling, wpInfoAfter, wpFinished

  //auto install 
  if PageIndexFromID(CurPageID) < PageIndexFromID(wpReady)  then 
  begin 
    if RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\white.exe', 'Path', InstallPath) then begin 
    
      InstallDirPage.Values[0] := InstallPath
    end else 
    begin 
      InstallDirPage.Values[0] := ''
    end;
  end;
  
end;

function NextButtonClick(CurPageID: Integer): Boolean;
begin
  //path example label 
  DataDirExampleLabel := TNewStaticText.Create(InstallDirPage);
  with DataDirExampleLabel do
  begin 
    Parent := InstallDirPage.Surface;
    Left := ScaleX(20);
    Top := 200
    Height:= 40
    Width  := InstallDirPage.SurfaceWidth; 
    Autosize := True;
    Wordwrap := True; 

    Caption :='Example: C:\Program Files (x86)\Lionhead Studios\Black and White 2                                       ';
    Font.Style := [fsBold]
  end;

  Result := True;
 
end;
 
//skip the component page! 
function ShouldSkipPage(PageID: Integer): Boolean;
begin
  Result := (PageID = wpSelectComponents);
end;

//Return the selected DataDir
function GetDestDir(Param: String): String;
begin
  Result := InstallDirPage.Values[0];
end;

procedure CancelButtonClick(CurPageID: Integer; var Cancel, Confirm: Boolean);
begin
  if CurPageID = wpInstalling then
  begin
    Cancel := False;
    if ExitSetupMsgBox then
    begin
      Cancel := True;
      Confirm := False;
      PercentLabel.Visible := False;
      ElapsedLabel.Visible := False;
      RemainingLabel.Visible := False;
    end;
  end;
end;

procedure CurInstallProgressChanged(CurProgress, MaxProgress: Integer);
var
  CurTick: DWORD;
begin
  CurTick := GetTickCount;
  PercentLabel.Caption :=
    Format('Done: %.2f %%', [(CurProgress * 100.0) / MaxProgress]);
  ElapsedLabel.Caption := 
    Format('Elapsed: %s', [TicksToStr(CurTick - StartTick)]);
  if CurProgress > 0 then
  begin
    RemainingLabel.Caption :=
      Format('Remaining: %s', [TicksToStr(
        ((CurTick - StartTick) / CurProgress) * (MaxProgress - CurProgress))]);
  end;
end;