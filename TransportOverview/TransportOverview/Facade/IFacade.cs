using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Facade {
	public interface IFacade {
		IFacadeFactory Facades { get; }
	}
}
