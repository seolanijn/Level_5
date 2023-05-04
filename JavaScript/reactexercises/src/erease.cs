public class Stack <T> { public <T> X; }
Stack<int> myStack_int = new Stack<int>();
Stack<string> myStack_Str = new Stack<string>();
myStack_int.Push(4.3);  // Compiler error
myStack_Str.set(“Hello”) ;

public static bool AreEqual(object val1, object val2)
{
    return val1 == val2;
}




public delegate void Mydelegate(int X); // declare 
Mydelegate del = new Mydelegate (Add); // Set a target method
static void Add(int x,int y)
{
    Console.writeline(X+y);
}
del.Add(10,20);
