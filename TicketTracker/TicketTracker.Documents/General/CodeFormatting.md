# Code Formatting

This document will set out the general style that the code should follow. In general we should be trying to stick to the SOLID principles and trying to limit large classes and large methods.

The following code block will demonstrate the general idea that classes should try to follow where possible:

```cs
{
    public class Foo
    {
        // Private fields at the top
        // These should be prefixed with an underscore _
        private string _value;

        // Use of pascalCase for private fields
        private string _valueExampleName;

        // Anything that is being handled by DI should be marked as readonly
        private readonly ILogging _logger;

        public Foo(ILogger logger)
        {
            _logger = logger;
        }

        // Public properties next

        public string UserName { get; set; }

        // Use of CamalCase for Public properties
        public int AgeOfPerson { get; set; }

        // Now for methods
        // These should be in CamalCase
        public string ExampleMethodName()
        {
            // Use of the keyword var
            // Try to use result as the name for any result value for a method
            var result = "result";
            return result;
        }

        // When there is multiple arguments into a method
        // Or even a constructor set them out like this
        // Makes it easier to follow
        public void BarTwo
        (
            string valueOne,
            int valueTwo,
            object valueThree
        )
        {

        }
    }
}
```

Also try to limit the use of words like Service, Manager etc. If you have a code block that is checking whether a user is in a role or not consider using something like ExistingUser as a name and not a generic UserManager class name.

<br/>

## Commenting

---

Where possible try to let your code do the commenting. Use verbose class and method names. Try to limit use of comments to bits of code that you can't break down any further.

For example:

```cs
{
    public string IsNumberEven(int valueOne)
    {
        if(valueOne % 2 == 0)
        {
            return "Is Even";
        }

        return "Is Odd";
    }

    // This could be written as

    public string IsNumberEven(int value)
    {
        if(isEven(value))
        {
            return "Is Even";
        }

        return "Is Odd";
    }

    private bool IsEven(int value)
    {
        return value % 2 == 0;
    }
}
```

The second way is more verbose and conveys more information.
Consider anything you have in an IF statement to be abstracted into a private method. This will tell the reader what you are checking without them having to take a moment to figure it out. Hence will cut down on the need for comments.

Another comment that should be avoided is:

```cs
{
    // Gets and Sets the UserName
    public string UserName { get; set; }
}
```

This kind of comment is pointless and contributes nothing to the code base.
