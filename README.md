# 1bitatatime Toolchain (v1.0.0)

Welcome to the official stable release of the **1bitatatime** language ecosystem. This repository houses the entire toolchain for a minimal, ultra-fast hardware gate simulator. It features a high-efficiency C# transpiler, an integrated VS Code syntax highlighter, and performance-optimized reference circuits.

The runtime simulator enforces a strict **64-bit virtual hardware environment** mapped entirely within a single physical CPU machine word (`unsigned long ram`), providing zero RAM allocation overhead during active execution layers.


## repository structure

- `src/`: source code
- `tests/`: unit tests
- `examples/`: example code
- `vscode/benjaminfberger.1bitatatime-1.0.0`: vscode syntax highlighting extension
- `docs/`: documentation on hardware contraints and language structure
- `misc/`: old c code

## examples

### example: swap values x and y ([examples/swap.1bit](/examples/swap.1bit))

```text
in: x y
temp = x
x = y
y = temp
out: x y
```

### example: x xor y ([examples/xor.1bit](/examples/xor.1bit))

```text
in: x y
w = x !& y
x = x !& w
y = y !& w
x = x !& y
out: x
```
Check out more [examples](/examples/examples.md). 

## compiling

To compile a source file using the 1bit transpiler, run
```bash
./1bit swap.1bit

# Expected output
success[1b000]: compiled binary: swap.exe
```

### running the generated binary
The compiler will create a binary. Execute it by passing space separated binary values (`1` or `0`) corresponding to your `in:` variables:

```bash
# Running with inputs x=1, y=0
./swap 1 0

# Expected Console Output:
output: 0 1
```

For comprehensive information on errors or warnings, or if you just want to learn more, refer to the [documentation](/docs/docs.md).
