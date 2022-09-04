# CatLang

CatLang is an experimental scripting language that runs on C#.

### TODO

[*] Variables
[] Math
[] Statements
[] Loops
[] Functions
[] Modules

### Examples

This is currently working:
```catlang
set:myvar Hello, world!
write:myvar
read:usr_input
write:usr_input
```

I hope this is going to be working:
```catlang
set:max_num 100
set min_num 1
set:random_number 0
math:random_number rand $max_num $min_num

set:msg Try to find the random number between $max_num and $min_num
write:msg

set:found false
set:tries 0

while: found==false
	read:input
	if: input > random_number
		set:msg Entered a higher number
	else if: input < random_number
		set:msg Entered a lower number
	else:
		set:msg You found it! Tried $tries times
		set:found true
	end if
	write:msg
	math: tries add 1
end while
```

## Contributing

The best thing you can do is repoting bugs and issues. Also you can create pull requests.