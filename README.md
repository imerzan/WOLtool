# WOLtool
.NET Core Wake-On-Lan console utility.

## Instructions:

1) Make sure the remote computer has Wake-On-Lan enabled in the System BIOS, and the remote network adapter supports WOL and has it enabled.
2) Startup WOLtool, provide MAC Address of the remote computer's network card.
   - Be sure you have the [.NET6](https://dotnet.microsoft.com/download) or newer runtime installed.
   - *(Optional)* You can run WOLtool from the command line: ```woltool <MacAddress1> <MacAddress2> <MacAddress3>...``` Multiple MAC Addresses are supported.
3) If Magic Packet broadcast is successful, you will receive a 'OK' message. As long as the remote computer is properly configured, it should wake ([See Considerations](https://github.com/imerzan/WOLtool#considerations)).

**NOTE:** To run on a macOS/Linux system you will need to run this from terminal/bash. Be sure you set the file permissions for WOLtool to be executable via terminal:
```bash
chmod 755 woltool
./woltool [optional Args]
```

![Example](https://user-images.githubusercontent.com/42287509/114205267-baaf0400-991f-11eb-9b66-bf219d4c2737.jpg)

### Considerations:
- It is possible to get a 'Success' message, but the remote computer may not wake. This could be due to network configuration, or the remote computer's configuration (BIOS,etc.)
- WakeOnLan uses broadcasts. As such they will only reach computers in the local subnet. If the message is transmitted in network 192.168.1.0/24 , computers in 192.168.2.0/24 will not receive the broadcast by default.
