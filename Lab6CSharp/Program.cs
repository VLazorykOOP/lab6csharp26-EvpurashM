// See https://aka.ms/new-console-template for more information
/// <summary>
///  Top-level statements 
///  Код програми (оператори)  вищого рівня
/// </summary>
///
Console.WriteLine("Lab6 C# ");
AnyFunc();

/// <summary>
/// 
///  Top-level statements must precede namespace and type declarations.
/// At the top-level methods/functions can be defined and used
/// На верхньому рівні можна визначати та використовувати методи/функції
/// </summary>
void AnyFunc()
{
    Console.WriteLine(" Some function in top-level");
}
Console.WriteLine("Problems 1 ");
AnyFunc();
//  приклад класів
UserClass cl = new UserClass();
cl.Name = " UserClass top-level ";
User.UserClass cl2 = new();
cl2.Name = " UserClass namespace User ";




/// <summary>
/// 
/// Top-level statements must precede namespace and type declarations.
/// Оператори верхнього рівня мають передувати оголошенням простору імен і типу.
/// Створення класу(ів) або оголошенням простору імен є закіченням  іструкцій верхнього рівня
/// 
/// </summary>

namespace User
{
    class UserClass
    {
        public string Name { get; set; }
        public UserClass()
        {
            Name = "NoName";
        }
        UserClass(string n)
        {
            Name = n;
        }
    }

}
class UserClass
{
    public string Name { get; set; }
}