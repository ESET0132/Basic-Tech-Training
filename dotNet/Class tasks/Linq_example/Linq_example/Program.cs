
List<string> names = new List<string> { "John", "Jane", "Steve", "Alice", "Bob", "joker", "Jorawar" };


IEnumerable<string> query = names.Where(name => name.StartsWith("J"));


foreach (string name in query)
{
    Console.WriteLine(name);
}