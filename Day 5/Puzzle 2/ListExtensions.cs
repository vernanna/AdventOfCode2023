namespace Puzzle_1;

public static class ListExtensions
{
    public static List<T> PopWhile<T>(this List<T> list, Func<T, bool> predicate)
    {
        var items = list.TakeWhile(predicate).ToList();
        list.RemoveRange(0, items.Count);

        return items;
    }

    public static T PopFirst<T>(this List<T> list)
    {
        var firstItem = list.First();
        list.RemoveAt(0);

        return firstItem;
    }

    public static void RemoveWhile<T>(this List<T> list, Func<T, bool> predicate)
    {
        var items = list.TakeWhile(predicate).ToList();
        list.RemoveRange(0, items.Count);
    }
}