ipconfig/flushdns
ipconfig/release
ipconfig/renew
netsh interface ipv4 set dns name=Ethernet static DNSIP
rmdir /s %Temp% /q
mkdir %Temp%
%windir%\SysWOW64\rundll32.exe advapi32.dll,ProcessIdleTasks