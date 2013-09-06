using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using System.Web.Routing;

namespace Server
{
	public class HttpServer
	{
		private readonly HttpListener _listener;
		private readonly IObservable<RequestContext> _stream;

		public HttpServer(string url)
		{
			_listener = new HttpListener();
			_listener.Prefixes.Add(url);
			_listener.Start();
			_stream = ObservableHttpContext();
		}

		private IObservable<RequestContext> ObservableHttpContext()
		{
			return Observable.Create<RequestContext>(obs =>
									  Observable.FromAsyncPattern<HttpListenerContext>(_listener.BeginGetContext, _listener.EndGetContext)()
													.Select(c => new RequestContext(c.Request, c.Response))
													.Subscribe(obs))
								  .Repeat()
								  .Retry()
								  .Publish()
								  .RefCount();
		}
	}
}
