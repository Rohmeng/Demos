using System;
namespace SwaggerAutomapperDemo.Models
{
    //嵌套映射
    public class OuterSource
    {
        public int Value { get; set; }
        public InnerSource Inner { get; set; }
    }
    public class InnerSource
    {
        public int OtherValue { get; set; }
    }
    public class OuterDest
    {
        public int Value { get; set; }
        public InnerDest Inner { get; set; }
    }
    public class InnerDest
    {
        public int OtherValue { get; set; }
    }
}
