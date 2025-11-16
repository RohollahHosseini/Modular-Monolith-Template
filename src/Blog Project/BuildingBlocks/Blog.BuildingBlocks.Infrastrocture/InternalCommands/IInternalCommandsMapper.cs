namespace Blog.BuildingBlocks.Infrastrocture.InternalCommands
{
    public interface IInternalCommandsMapper
    {
        string GetName(Type type);

        Type GetType(string name);

    }
}
