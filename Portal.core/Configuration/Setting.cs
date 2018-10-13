using QtasMarketing.Core;

namespace Portal.core.Configuration
{
  public  class Setting:BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Setting() { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="value">Value</param>
    
        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
           
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string Value { get; set; }

    

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>Result</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
