#!/usr/bin/env python3

from subprocess import Popen, PIPE

DOTNET_EXE = "dotnet"
EXE = "run"

with Popen([DOTNET_EXE, EXE, "-version"], stdout=PIPE) as proc:
    data = proc.stdout.read().decode()
    assert data.count(".") == 2
    assert data.startswith("TreasureChest")

with Popen([DOTNET_EXE, EXE], stdout=PIPE) as proc:
    data = proc.stdout.read().decode()
    assert data.count(".") == 2
    assert data.startswith("TreasureChest")
    assert "Run with help for more options" in data

print("All tests passed")
