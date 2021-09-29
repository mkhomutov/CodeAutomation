namespace CodeAutomationConsole
{
    using System;
    using System.Linq;
    
    // Keep fieldname from csv and transform it into _field or Property or constructor using Variable 
    public class CsvField
    {
        private readonly string _field;

        public CsvField(string field)
        {
            _field = field;
        }

        public string Field
        {
            get => "_" + _field[0].ToString().ToLower() + String.Join("", _field.Skip(1));
        }

        public string Property
        {
            get => _field[0].ToString().ToUpper() + String.Join("", _field.Skip(1));
        }

        public string ConstructorInput
        {
            get => _field[0].ToString().ToLower() + String.Join("", _field.Skip(1));
        }

    }
}
