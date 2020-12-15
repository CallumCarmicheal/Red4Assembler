# Red4Assembler

This project aims to create a dissembler and a compiler for CP77 RED4 Scripts.

Currently the project is focused on dissembling the scripts and after start implementing a compiler as that is the more complex project. 

This project is very early stages, don't expect it to work or be maintained very well. As more work is done the code-base will be improved and refactored.

# License

This project is using the [zlib license](https://choosealicense.com/licenses/zlib/) and heavily makes use of [Gibbed's RED4 library](https://github.com/gibbed/Gibbed.RED4) for processing the script opcodes's.
Thanks to [Gibbed](https://github.com/gibbed) for creating such a useful library very early on. 

# How to compile

1. Clone the github repository and recursively include git modules:
```sh
git clone --recurse-submodules -j8 https://github.com/CallumCarmicheal/Red4Assembler.git
```
2. Open `Red4Assembler.sln` in Visual Studio.
3. Build all projects.
4. Open `projects\Red4Assembler\bin\{Debug|Release}`
5. Launch `Red4Assembler.exe` with the path to `Cyberpunk 2077\r6\cache\final.redscripts` or Copy `final.redscripts` into the current folder and just run `Red4Assembler.exe`.
6. Open the folder `projects\Red4Assembler\bin\{Debug|Release}\Red4Assembler\debug` to see the output.

# Contributing

There are currently no rules for contributing as the source code is a mess, If you think you have something to add; fork the repository and create a pull request. `Embrace the chaos.`