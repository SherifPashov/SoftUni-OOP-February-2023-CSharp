using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using UniversityCompetition;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_000
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateApplyToUniversity_ValidParameters()
    {
        var controller = CreateObjectInstance(GetType("Controller"));


        var subjectArgs1 = new object[] { "Mathematics", "TechnicalSubject" };
        var subjectArgs2 = new object[] { "Physics", "TechnicalSubject" };
        var subjectArgs3 = new object[] { "Geography", "EconomicalSubject" };
        var subjectArgs4 = new object[] { "Biology", "HumanitySubject" };
        var subjectArgs5 = new object[] { "Chemistry", "TechnicalSubject" };
        var subjectArgs6 = new object[] { "Literature", "HumanitySubject" };
        var subjectArgs7 = new object[] { "History", "EconomicalSubject" };
        var subjectArgs8 = new object[] { "ComputerProgramming", "TechnicalSubject" };

        InvokeMethod(controller, "AddSubject", subjectArgs1);
        InvokeMethod(controller, "AddSubject", subjectArgs2);
        InvokeMethod(controller, "AddSubject", subjectArgs3);
        InvokeMethod(controller, "AddSubject", subjectArgs4);
        InvokeMethod(controller, "AddSubject", subjectArgs5);
        InvokeMethod(controller, "AddSubject", subjectArgs6);
        InvokeMethod(controller, "AddSubject", subjectArgs7);
        InvokeMethod(controller, "AddSubject", subjectArgs8);


        List<string> subjectsGreatEinstein = new List<string>()
        {
            "Physics",
            "Mathematics",
            "ComputerProgramming"
        };
        List<string> subjectsIbnSena = new List<string>()
        {
            "Biology",
            "Phycics",
            "Chemistry"
        };

        var universityArgs1 = new object[] { "GreatEinstein", "Technical", 50, subjectsGreatEinstein };
        var universityArgs2 = new object[] { "IbnSena", "Humanity", 30, subjectsIbnSena };



        InvokeMethod(controller, "AddUniversity", universityArgs1);
        InvokeMethod(controller, "AddUniversity", universityArgs2);

        var studentArgs1 = new object[] { "Alice", "Pitt" };
        var studentArgs2 = new object[] { "Boris", "Grey" };
        var studentArgs3 = new object[] { "Rob", "Butterscotch" };


        InvokeMethod(controller, "AddStudent", studentArgs1);
        InvokeMethod(controller, "AddStudent", studentArgs2);
        InvokeMethod(controller, "AddStudent", studentArgs3);


        InvokeMethod(controller, "TakeExam", new object[] { 1, 4 });
        InvokeMethod(controller, "TakeExam", new object[] { 1, 2 });
        InvokeMethod(controller, "TakeExam", new object[] { 1, 5 });
        InvokeMethod(controller, "TakeExam", new object[] { 3, 2 });
        InvokeMethod(controller, "TakeExam", new object[] { 3, 1 });

        InvokeMethod(controller, "ApplyToUniversity", new object[] { "Alice Pitt", "GreatEinstein" });
        InvokeMethod(controller, "ApplyToUniversity", new object[] { "Alice Pitt", "IbnSena" });
        InvokeMethod(controller, "ApplyToUniversity", new object[] { "Rob Butterscotch", "GreatEinstein" });

        InvokeMethod(controller, "TakeExam", new object[] { 3, 8 });

        InvokeMethod(controller, "ApplyToUniversity", new object[] { "Rob Butterscotch", "GreatEinstein" });

        var actualResult = InvokeMethod(controller, "UniversityReport", new object[] { 1 });

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("*** GreatEinstein ***");
        sb.AppendLine("Profile: Technical");
        sb.AppendLine("Students admitted: 1");
        sb.AppendLine("University vacancy: 49");

        var expectedResult = sb.ToString().TrimEnd();

        Assert.AreEqual(expectedResult, actualResult);
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        try
        {
            var result = obj.GetType()
                .GetMethod(methodName)
                .Invoke(obj, parameters);

            return result;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static object CreateObjectInstance(Type type, params object[] parameters)
    {
        try
        {
            var desiredConstructor = type.GetConstructors()
                .FirstOrDefault(x => x.GetParameters().Any());

            if (desiredConstructor == null)
            {
                return Activator.CreateInstance(type, parameters);
            }

            var instances = new List<object>();

            foreach (var parameterInfo in desiredConstructor.GetParameters())
            {
                var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name.Contains(name));

        return type;
    }
}