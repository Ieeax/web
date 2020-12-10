using Microsoft.Extensions.Primitives;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;

namespace Leeax.Extensions.Web.Components
{
    // Base is taken from https://github.com/dotnet/aspnetcore/blob/9249a9528677f27b9e75725c337acee7bbbc7579/src/Http/Http.Extensions/src/QueryBuilder.cs
    public class QueryBuilder : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IList<KeyValuePair<string, string>> _params;

        public QueryBuilder()
        {
            _params = new List<KeyValuePair<string, string>>();
        }

        public QueryBuilder(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            _params = new List<KeyValuePair<string, string>>(parameters);
        }

        public QueryBuilder(IEnumerable<KeyValuePair<string, StringValues>> parameters)
            : this(parameters.SelectMany(kvp => kvp.Value, (kvp, v) => KeyValuePair.Create(kvp.Key, v)))
        {
        }

        public QueryBuilder Add(string key, IEnumerable<string> values)
        {
            key.ThrowIfNull();
            values.ThrowIfNull();

            foreach (var curValue in values)
            {
                _params.Add(new KeyValuePair<string, string>(key, curValue));
            }

            return this;
        }

        public QueryBuilder Add(string key, string value)
        {
            key.ThrowIfNull();
            value.ThrowIfNull();

            _params.Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        public QueryBuilder AddOrUpdate(string key, IEnumerable<string> values)
        {
            key.ThrowIfNull();
            values.ThrowIfNull();

            return Remove(key)
                .Add(key, values);
        }

        public QueryBuilder AddOrUpdate(string key, string value)
        {
            key.ThrowIfNull();
            value.ThrowIfNull();

            return Remove(key)
                .Add(key, value);
        }

        public QueryBuilder Remove(string key)
        {
            key.ThrowIfNull();

            for (int i = 0; i < _params.Count; i++)
            {
                if (_params[i].Key == key)
                {
                    _params.RemoveAt(i--);
                }
            }

            return this;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            bool first = true;
            for (int i = 0; i < _params.Count; i++)
            {
                var pair = _params[i];
                builder.Append(first ? "?" : "&");
                first = false;
                builder.Append(UrlEncoder.Default.Encode(pair.Key));
                builder.Append("=");
                builder.Append(UrlEncoder.Default.Encode(pair.Value));
            }

            return builder.ToString();
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return ToString().Equals(obj);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return _params.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _params.GetEnumerator();
        }
    }
}