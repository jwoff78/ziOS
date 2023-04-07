# ziOS X
ziOS is no longer supported and will be replaced with ziOS X.

# ziOS
<img src="https://img.shields.io/badge/Milestone-3-green?style=flat-square"> <img src="https://img.shields.io/badge/Real%20Hardware-FAIL-red?style=flat-square">

<img src="https://user-images.githubusercontent.com/49623720/230219229-6f801b8b-5143-489c-b34b-48ebc7e9d6d5.png">

A pretty unique operating system created with the Cosmos C# framework.

Real hardware support already has a planned solution and will be resolved in Milestone 5.

# Features (In M3)
- Shell with multi-arg support
- Shorter versions for commands in shell, for example LS could be typed as $
- A minimalistic replacement for MIV called ziv
- Unique mouse apps (console eraser and console drawing)
# Commands (In M3)
- Help or ?
- Shutdown or - [restart (-r)]
- Echo [msg]
- syslog
- ls or $
- cd or @ [directory]
- fwrite or * [file] [msg]
- cat or # [file] [hexadecimal (-x)]
- rm, del, or & [FS object (file or dir)] [recursive (-r)]
- mkdir [dir]
- ziv or % [file]
- scr
- cls or clear
- eraser
- condraw

# Building
Get the Cosmos dev kit. Before you build Cosmos, follow these instructions: 
- Replace the mentions of `80` and `25` in `line 27` and `line 82 & 83` in `Cosmos\source\Cosmos.HAL2\TextScreen.cs`.
- just build cosmos and open visual studio 2022 and load the project file and press build
