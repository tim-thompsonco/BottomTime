﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BottomTimeAuth.Models {
	public class AppUser : IdentityUser<int> {
		public ICollection<AppUserRole> UserRoles { get; set; }
	}
}
