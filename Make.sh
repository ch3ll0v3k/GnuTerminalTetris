
#mono-csc /target:library /out:math.dll MulClass.cs AddClass.cs
#mono-csc /out:MainProg.exe /reference:math.dll MainProg.cs


mono-csc /out:GnuTerminalTetris.exe ObjectsCreater.cs Tetris.cs GnuTerminalTetris.cs 
