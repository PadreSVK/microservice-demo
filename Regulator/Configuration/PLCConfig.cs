namespace Regulator.Configuration
{
    public class PLCConfig
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public void Deconstruct(out string name, out int age)
        {
            name = Name;
            age = Age;
            var test = new { Car = "asd" };
        }


        private (string test, int age) GetInfo(PLCConfig2 config2)
        {
            return config2 switch
            {
                { Age: > 15 } and { Name: "asd" } => (test: "asd", age: 11),
                { Age: > 10 } => (test: "asd", age: 11)
            };
        }
    }


    public record PLCConfig2(string Name, int Age);


    public class PLCConfig3
    {
        public PLCConfig3()
        {
            var plcConfig = new PLCConfig
            {
                Name = "asdasd"
            };

            var (name, age) = plcConfig;


            var plcConfig2 = new PLCConfig2("asda", 11);


            PLCConfig2 plcConfig3 = new("asda", 11);

            var ss = default(PLCConfig);

            //plcConfig2.Name = "aaaa";
        }
    }
}