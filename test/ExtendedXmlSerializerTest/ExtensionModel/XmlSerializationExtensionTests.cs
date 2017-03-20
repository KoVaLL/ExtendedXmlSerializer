﻿// MIT License
//
// Copyright (c) 2016 Wojciech Nagórski
//                    Michael DeMond
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Xml;
using ExtendedXmlSerialization.Configuration;
using ExtendedXmlSerialization.Test.Support;
using JetBrains.Annotations;
using Xunit;

namespace ExtendedXmlSerialization.Test.ExtensionModel
{
	public class XmlSerializationExtensionTests
	{
		[Fact]
		public void Write()
		{
			var serializer = new ExtendedConfiguration().WithSettings(new XmlWriterSettings {Indent =  true});
			var support = new SerializationSupport(serializer);
			support.Assert(new Subject {PropertyName = "Hello World!" }, @"<?xml version=""1.0"" encoding=""utf-8""?>
<XmlSerializationExtensionTests-Subject xmlns=""clr-namespace:ExtendedXmlSerialization.Test.ExtensionModel;assembly=ExtendedXmlSerializerTest"">
  <PropertyName>Hello World!</PropertyName>
</XmlSerializationExtensionTests-Subject>");
		}

		class Subject
		{
			public string PropertyName { [UsedImplicitly] get; set; }
		}
	}
}