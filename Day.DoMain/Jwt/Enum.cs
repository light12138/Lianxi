using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.DoMain.Jwt
{
   public enum HeaderName
    {
        [Description("typ")]
        Type,

        [Description("cty")]
        ContentType,


        [Description("alg")]
        Algorithm,


        [Description("kid")]
        KeyId
    }

    public enum ClaimName
    {
        [Description("iss")]
        Issuer,

        [Description("sub")]
        Subject,

        [Description("aud")]
        Audience,

        [Description("exp")]
        ExpirationTime,

        [Description("nbf")]
        NotBefore,

        [Description("iat")]
        IssuedAt,

        [Description("jti")]
        JwtId,

        [Description("name")]
        FullName,

        [Description("given_name")]
        GivenName,

        [Description("family_name")]
        FamilyName,

        [Description("middle_name")]
        MiddleName,

        [Description("nickname")]
        CasualName,

        [Description("preferred_username")]
        PreferredUsername,

        [Description("profile")]
        ProfilePageUrl,

        [Description("picture")]
        ProfilePictureUrl,

        [Description("website")]
        Website,

        [Description("email")]
        PreferredEmail,

        [Description("email_verified")]
        VerifiedEmail,

        [Description("gender")]
        Gender,

        [Description("birthdate")]
        Birthday,

        [Description("zoneinfo")]
        TimeZone,

        [Description("locale")]
        Locale,

        [Description("phone_number")]
        PreferredPhoneNumber,

        [Description("phone_number_verified")]
        VerifiedPhoneNumber,

        [Description("address")]
        Address,

        [Description("update_at")]
        UpdatedAt,

        [Description("azp")]
        AuthorizedParty,

        [Description("nonce")]
        Nonce,

        [Description("auth_time")]
        AuthenticationTime,

        [Description("at_hash")]
        AccessTokenHash,

        [Description("c_hash")]
        CodeHashValue,

        [Description("acr")]
        Acr,

        [Description("amr")]
        Amr,

        [Description("sub_jwk")]
        PublicKey,

        [Description("cnf")]
        Confirmation,

        [Description("sip_from_tag")]
        SipFromTag,

        [Description("sip_date")]
        SipDate,

        [Description("sip_callid")]
        SipCallId,

        [Description("sip_cseq_num")]
        SipCseqNumber,

        [Description("sip_via_branch")]
        SipViaBranch,

        [Description("orig")]
        OriginatingIdentityString,

        [Description("dest")]
        DestinationIdentityString,

        [Description("mky")]
        MediaKeyFingerprintString
    }


    public enum JwtHashAlgorithm
    {
        /// <summary>
        /// HMAC using SHA-256
        /// </summary>
        HS256,
        /// <summary>
        /// HMAC using SHA-384
        /// </summary>
        HS384,
        /// <summary>
        /// HMAC using SHA-512
        /// </summary>
        HS512,
        /// <summary>
        /// RSASSA-PKCS1-v1_5 using SHA-256
        /// </summary>
        RS256
    }

    public enum JwtPartsIndex
    {
        Header = 0,
        Payload = 1,
        Signature = 2
    }
}
