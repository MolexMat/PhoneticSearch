BT Software Coding Exercise 2014: Phonetic Search
=======

Introduction
-----------

An application created by Mat Kastner for the BT graduates application process. 

This application reads a list of surnames from standard input, one per line. The command-line arguments are to be one or more surnames. For each of these surnames the application prints out all of the names in the input list that match those on the command-line according to the following matching algorithm:

1. All non-alphabetic characters are ignored
2. Word case is not significant 
3. After the first letter, any of the following letters are discarded: A, E, I, H, O, U, W, Y. 
4. The following sets of letters are considered equivalent 
  * A, E, I, O, U 
  * C, G, J, K, Q, S, X, Y, Z 
  * B, F, P, V, W 
  * D, T 
  * M, N
  * All others have no equivalent
5. Any consecutive occurrences of equivalent letters (after discarding letters in step 3) are considered as a single occurrence

Building instructions
-----------

Use **Microsoft Visual Studio Express 2013 for Windows Desktop** to open **PhoneticSearch.sln**. Press F7 to build. If built using the debug configuration, the application will display how long it took to calculate the matches.

Operating instructions
-----------

Open a command prompt window and navigate to the folder that contains the application. Run it by typing 'PhoneticSearch [arguments] < [textfile]', for example 'PhoneticSearch Jones Winton < surnames.txt'. By default a copy of surnames.txt will be copied into the build folder during the build process.

File manifest
-----------

* README.md
* PhoneticSearch.sln
* PhoneticSearch/App.config
* PhoneticSearch/PhoneticSearch.csproj
* PhoneticSearch/Program.cs
* PhoneticSearch/surnames.txt
* PhoneticSearch/Properties/AssemblyInfo.cs
