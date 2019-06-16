namespace Cafeteria.SharedView.Services
{
    public class ConstructorParameter
    {
        public ConstructorParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public object Value { get;}
    }
}