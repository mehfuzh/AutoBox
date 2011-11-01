//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Machine.Specifications;

//namespace AutoConfig.Test
//{
//    [Subject(typeof(AutoConfig), "Resolving complex type")]
//    public class when_type_is_scaffolded
//    {
//        readonly Establish context = () => AutoConfig.Init();

//        readonly Because of = () => controller = utoConfig.Resolve<ProductController>();

//        readonly It should_not_be_null = () => controller.ShouldNotBeNull();

//        readonly It should_resolve_the_dependencies = () => controller.CreateNewProduct().ShouldNotBeNull();

//        static ProductController controller;
//    }
//}
