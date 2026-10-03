# 1bitatatime example code

### swap values x and y ([swap.1bit](/examples/swap.1bit))

```text
in: x y
temp = x
x = y
y = temp
out: x y
```

### x xor y ([xor.1bit](/examples/xor.1bit))

```text
in: x y
w = x !& y
x = x !& w
y = y !& w
x = x !& y
out: x
```

### x or y ([or.1bit](/examples/or.1bit))

```text
in: x y
x = x !& x
y = y !& y
x = x !& y
out: x
```

### x and y ([and.1bit](/examples/and.1bit))

```text
in: x y
x = x !& y
x = x !& x
out: x
```

### not x ([not.1bit](/examples/not.1bit))

```text
in: x
x = x !& x
out: x
```