namespace ConsoleApp.Models
{
    public class GeneralPerson
    {
        public DateOnly DateOfBirth { get; set; }

        private string _surname;

        public required string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                value = value.Trim();
                var prvni = new string(value.Take(1).ToArray()).ToUpper();
                var zbytek = value.Substring(1).ToLower();
                _surname = prvni + zbytek;
            }
        }


        public int Age()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - DateOfBirth.Year;
            if (today < DateOfBirth.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
