using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeAutomationConsole.Tests.TemplateTranslators
{
    public class ReflectionTemplateTranslatorTests
    {
        private class Model
        {
            public string SingleString { get; set; }
            public Model SingleNested { get; set; }
            public List<string> MultipleStrings { get; set; }
            public List<Model> MultipleNested { get; set; }
            public object DynamicDictionary { get; set; }
        }

        private Model CreateModel()
        {
            var model = new Model
            {
                SingleString = "single string value",
                SingleNested = new Model
                {
                    SingleString = "nested string value",
                    MultipleStrings = new List<string>
                    {
                        "single nested value 1",
                        "single nested value 2",
                        "single nested value 3",
                    }
                },
                MultipleStrings = new List<string>
                {
                    "value 1",
                    "value 2",
                    "value 3"
                },
                MultipleNested = new List<Model>
                {
                    new Model
                    {
                        SingleString = "multiple nested single string 1",
                        MultipleStrings = new List<string>
                        {
                            "multiple nested multiple string 1",
                            "multiple nested multiple string 2",
                            "multiple nested multiple string 3",
                        }
                    },
                    new Model
                    {
                        SingleString = "multiple nested single string 2",
                        MultipleStrings = new List<string>
                        {
                            "multiple nested multiple string 4",
                            "multiple nested multiple string 5",
                            "multiple nested multiple string 6",
                        }
                    },
                    new Model
                    {
                        SingleString = "multiple nested single string 3",
                        MultipleStrings = new List<string>
                        {
                            "multiple nested multiple string 7",
                            "multiple nested multiple string 8",
                            "multiple nested multiple string 9",
                        }
                    }
                },
                DynamicDictionary = new Dictionary<object, object>()
                {
                    {"single", "single value"},
                    {"multiple", new List<object>{ "value 1", "value 2", "value 3"}},
                    {"nested", new Dictionary<object, object>
                        {
                            {"single", "nested single value"}, // TODO: add test case
                            {"multiple", new List<string> {"nested value 1", "nested value 2", "nested value 3"}} // TODO: add test case
                        }
                    },
                }
            };

            return model;
        }

        [Test]
        public void CanGetSingleStringValue()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = nameof(Model.SingleString)
            };

            var results = translator.Translate(translationContext);

            var singleResult = results.Single();

            Assert.AreEqual(model.SingleString, singleResult.Value);
            Assert.AreEqual(model, singleResult.Context);
        }

        [Test]
        public void CanGetMultipleStringValues()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = nameof(Model.MultipleStrings)
            };

            var results = translator.Translate(translationContext);

            Assert.AreEqual(results.Count, model.MultipleStrings.Count);

            foreach (var result in results)
            {
                Assert.Contains(result.Value, model.MultipleStrings);
                Assert.AreEqual(model, result.Context);
            }
        }

        [Test]
        public void CanGetSingleNestedSingleStringValue()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.SingleNested)}.{nameof(Model.SingleString)}"
            };

            var results = translator.Translate(translationContext);

            var singleResult = results.Single();

            Assert.AreEqual(model.SingleNested.SingleString, singleResult.Value);
            Assert.AreEqual(model.SingleNested, singleResult.Context);
        }

        [Test]
        public void CanGetSingleNestedMultipleStringValue()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.SingleNested)}.{nameof(Model.MultipleStrings)}"
            };

            var results = translator.Translate(translationContext);

            Assert.AreEqual(results.Count, model.SingleNested.MultipleStrings.Count);

            foreach (var result in results)
            {
                Assert.Contains(result.Value, model.SingleNested.MultipleStrings);
                Assert.AreEqual(model.SingleNested, result.Context);
            }
        }

        [Test]
        public void CanGetMultipleNestedSingleStringValues()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.MultipleNested)}.{nameof(Model.SingleString)}"
            };

            var results = translator.Translate(translationContext);

            var expectedValues = model.MultipleNested.Select(x => x.SingleString).ToList();
            var expectedContextByValue = model.MultipleNested.ToDictionary(x => x.SingleString, x => x);

            Assert.AreEqual(results.Count, expectedValues.Count);

            foreach (var result in results)
            {
                Assert.Contains(result.Value, expectedValues);
                Assert.AreEqual(expectedContextByValue[(string)result.Value], result.Context);
            }
        }

        [Test]
        public void CanGetMultipleNestedMultipleStringValues()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.MultipleNested)}.{nameof(Model.MultipleStrings)}"
            };

            var results = translator.Translate(translationContext);


            var expectedValues = model.MultipleNested.SelectMany(x => x.MultipleStrings).ToList();
            var expectedContextByValue = model.MultipleNested
                .SelectMany(x => x.MultipleStrings.Select(y => new { Context = x, Value = y }))
                .ToDictionary(x => x.Value, x => x.Context);

            Assert.AreEqual(results.Count, expectedValues.Count);

            foreach (var result in results)
            {
                Assert.Contains(result.Value, expectedValues);
                Assert.AreEqual(expectedContextByValue[(string)result.Value], result.Context);
            }
        }

        [Test]
        public void CanGetSingleStringDynamicValues()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.DynamicDictionary)}.single"
            };

            var results = translator.Translate(translationContext);

            var singleResult = results.Single();

            var dictionary = (Dictionary<object, object>)model.DynamicDictionary;

            Assert.AreEqual(dictionary["single"], singleResult.Value);
            Assert.AreEqual(dictionary, singleResult.Context);
        }

        [Test]
        public void CanGetMultipleStringDynamicValue()
        {
            var translator = new ValueResolver();

            var model = CreateModel();

            var translationContext = new TranslationContext
            {
                Context = model,
                Argument = $"{nameof(Model.DynamicDictionary)}.multiple"
            };

            var results = translator.Translate(translationContext);

            var dictionary = (Dictionary<object, object>)model.DynamicDictionary;

            var multipleValues = (List<object>)dictionary["multiple"];
            var expectedValues = multipleValues.Select(x => x).ToList();

            Assert.AreEqual(results.Count, expectedValues.Count);

            foreach (var result in results)
            {
                Assert.Contains(result.Value, expectedValues);
                Assert.AreEqual(dictionary, result.Context);
            }
        }
    }
}
