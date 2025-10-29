using System.Xml;
using OpenGLSharp;

XmlDocument specs = new();
specs.Load("specs.xml");
File.WriteAllText("Enums.cs", TypeGenerator.Generate(specs));
File.WriteAllText("GL.cs", CommandGenerator.Generate(specs));
