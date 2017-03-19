// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
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

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using ExtendedXmlSerialization.Core.Sources;

namespace ExtendedXmlSerialization.ContentModel.Converters
{
	class Converters : CacheBase<TypeInfo, IConverter>, IConverters
	{
		readonly ImmutableArray<IConverter> _converters;
		readonly ImmutableArray<IConverterSource> _sources;

		public Converters(IEnumerable<IConverter> converters, IEnumerable<IConverterSource> sources)
		{
			_converters = converters.ToImmutableArray();
			_sources = sources.ToImmutableArray();
		}

		public bool IsSatisfiedBy(TypeInfo parameter) => Get(parameter) != null;

		protected override IConverter Create(TypeInfo parameter)
		{
			foreach (var converter in _converters)
			{
				if (converter.IsSatisfiedBy(parameter))
				{
					return converter;
				}
			}

			foreach (var source in _sources)
			{
				if (source.IsSatisfiedBy(parameter))
				{
					return source.Get(parameter);
				}
			}
			return null;
		}
	}
}