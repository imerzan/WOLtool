Instructions:

1) Make sure the remote computer has Wake-On-Lan enabled in the System BIOS, and the remote network adapter supports WOL and has it enabled.

2) Make sure you have the .NET Core Runtime (3.1 or newer) installed on the computer that will run WOLtool, see https://dotnet.microsoft.com/download

3) Startup WOLtool, provide MAC Address of the remote computer's network card.
  optional: You can also run the program from the command line: WOLtool.exe <MACAddress>
  
4) If Magic Packet broadcast is successful, you will receive a "Success!" message. As long as the remote computer is properly configured, it should wake.


macOS/Linux NOTE: To run on a macOS/Linux system you will need to run this from terminal. Be sure you set the file permissions for WOLtool to be executable via terminal:
  1) From terminal navigate to the folder that houses WOLtool
  2) Run the command: chmod 755 WOLtool
  3) Run WOLtool: sudo .\WOLtool
