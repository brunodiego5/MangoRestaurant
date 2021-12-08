// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


namespace Mango.Services.Identity.Device;

public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
