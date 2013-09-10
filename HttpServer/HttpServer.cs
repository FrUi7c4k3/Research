using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Text;
using System.Web.Routing;

namespace Server
{
	/// <summary>
	/// See:
	/// http://joseoncode.com/2011/06/17/event-driven-http-server-in-c-with-rx-and-httplistener/	- Explanation on blog
	/// https://bitbucket.org/jfromaniello/asynchttplistener													- Code for this example
	/// https://github.com/jfromaniello/Anna																		- Follow up on this with an actual implementation
	/// http://joseoncode.com/2011/07/22/long-polling-chat-with-anna/										- Follow on the previous implementation
	/// </summary>
	
	public class HttpServer : IObservable<RequestContext>
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
			//_listener.BeginGetContext(ar =>	{
			//                                    var state = ar.AsyncState as HttpListener;
			//                                    HttpListenerContext context = state.EndGetContext(ar);
			//                                 }, _listener);
			Func<IObservable<HttpListenerContext>> source = Observable.FromAsyncPattern<HttpListenerContext>(_listener.BeginGetContext, _listener.EndGetContext);
			IObservable<RequestContext> stream = Observable.Create<RequestContext>(obs => source().Subscribe(obs())); // WTF ?!
			
			return Observable.Create<RequestContext>(obs => 
												Observable.FromAsyncPattern<HttpListenerContext>(_listener.BeginGetContext, _listener.EndGetContext)()
													.Select(c => new RequestContext(c.Request, c.Response))
													.Subscribe(obs))
								  .Repeat()
								  .Retry()
								  .Publish()
								  .RefCount();
		}

		public IDisposable Subscribe(IObserver<RequestContext> observer)
		{
			throw new NotImplementedException();
		}
	}
}
