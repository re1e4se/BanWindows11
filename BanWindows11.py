import ctypes, sys
import winreg
import platform

if platform.system() != "Windows":
    print('[ERROR] This script can only run on Windows.')
    exit()

# Function to check if the script is running with administrator privileges
def is_admin():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        return False

if is_admin() == False:
    print('[ERROR] This script requires administrator privileges. Starting with administrator privileges...')
    ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, " ".join(sys.argv), None, 1)
    exit()
    
try:
    # Open the key
    key = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate")

    # Get the value of the "ProductVersion" registry key and assign it to product_version variable
    product_version = winreg.QueryValueEx(key, "ProductVersion")[0]

    # Get the value of the "TargetReleaseVersion" registry key and assign it to target_release_version variable
    target_release_version = winreg.QueryValueEx(key, "TargetReleaseVersion")[0]

    # Get the value of the "TargetReleaseVersionInfo" registry key annd assign it to target_release_version_info variable
    target_release_version_info = winreg.QueryValueEx(key, "TargetReleaseVersionInfo")[0]

    # Close the key
    winreg.CloseKey(key)

except WindowsError:
    print('[WARNING] Registry keys not found. Creating...')

    # Create new key
    key = winreg.CreateKey(winreg.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate")

    # Set the value of the "ProductVersion" registry key
    winreg.SetValueEx(key, "ProductVersion", 0, winreg.REG_SZ, "Windows 10")

    # Set the value of the "TargetReleaseVersion" registry key
    winreg.SetValueEx(key, "TargetReleaseVersion", 0, winreg.REG_DWORD, "1")

    # Set the value of the "TargetReleaseVersionInfo" registry key
    winreg.SetValueEx(key, "TargetReleaseVersionInfo", 0, winreg.REG_SZ, "22H2")

    # Close the key
    winreg.CloseKey(key)

    print('[INFO] Registry keys created.')

# Function to print the actions
def printActions():
    print('BanWindows11 - Python Port')
    print('-------------------------------')
    print('Please select an action to perform:')
    print('1) Disable Windows 11 Upgrade')
    print('2) Enable Windows 11 Upgrade')
    
    # Get user input
    action = input('\n> ')

    # Check if the user input is valid
    try:
        int(action)
    except:
        print('Please enter a valid action. Example: 1')
        printActions()
    if int(action) == 1:
        target_release = input('Please type the Target Release (Example: 22H2): ')

        # Open the key
        key = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate")

        # Set the value of the "ProductVersion" registry key
        winreg.SetValueEx(key, "ProductVersion", 0, winreg.REG_SZ, "Windows 10")

        # Set the value of the "TargetReleaseVersion" registry key
        winreg.SetValueEx(key, "TargetReleaseVersion", 0, winreg.REG_DWORD, "1")

        # Set the value of the "TargetReleaseVersionInfo" registry key
        winreg.SetValueEx(key, "TargetReleaseVersionInfo", 0, winreg.REG_SZ, target_release)

        # Close the key
        winreg.CloseKey(key)

        # Wait for user to press enter to exit
        input('Press Enter to exit...')
    elif int(action) == 2:
        # Open the key
        key = winreg.OpenKey(winreg.HKEY_LOCAL_MACHINE, "SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate")

        # Set the value of the "ProductVersion" registry key
        winreg.SetValueEx(key, "ProductVersion", 0, winreg.REG_SZ, "Windows 10")

        # Set the value of the "TargetReleaseVersion" registry key
        winreg.SetValueEx(key, "TargetReleaseVersion", 0, winreg.REG_DWORD, "0")

        # Set the value of the "TargetReleaseVersionInfo" registry key
        winreg.SetValueEx(key, "TargetReleaseVersionInfo", 0, winreg.REG_SZ, "22H2")

        # Close the key
        winreg.CloseKey(key)

        # Wait for user to press enter to exit
        input('Press Enter to exit...')
    else:
        print('Please enter a valid action. Example: 1')
        printActions()

printActions()
