% this is a function that adds 2 numbers
funtion [result] = ExampleFunction(num1, num2)

%if you want to put stuff into the workspace, do so like this ('base', [name of variable], [value]):
assignin('base', "zero", 0);
%if you want to retrieve it again for use in the function ('base', [name of variable]):
zero = evalin('base', "zero");
%return the result
result = num1 + num2 + zero;