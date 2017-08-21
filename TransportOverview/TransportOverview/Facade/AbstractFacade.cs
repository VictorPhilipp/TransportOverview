using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Facade {
	public class AbstractFacade : IFacade {
		public IFacadeFactory Facades {
			get {
				return Constants.FacadeFactory;
			}
		}
	}
}
