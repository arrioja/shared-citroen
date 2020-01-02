<?xml version="1.0" ?>
<configuration>
	<configSections>
		<section name="Esperantus" type="System.Configuration.NameValueSectionHandler, System, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
		<section name="BankGateways" type="System.Configuration.NameValueSectionHandler, System, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
		<section name="ShippingObjects" type="System.Configuration.NameValueSectionHandler, System, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
		<sectionGroup name="Rewrite.NET">
			<!--index entry is required-->
			<section name="Index" type="System.Configuration.NameValueSectionHandler,System,Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
			<!--This setting is required. It contains general settings for the Rules Engine -->
			<section name="Settings" type="System.Configuration.SingleTagSectionHandler,System,Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
			<!--each optional section name needs to be defined for each rule/section you want-->
			<!--you can have more than one section, with the same rule (SimpleRule, etc..)-->
			<!-- Comment out if you do not have the RewriteRules.Rainbow Assembly in the bin directory-->
			<section name="RainbowDefaultRule" type="System.Configuration.SingleTagSectionHandler,System,Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
			<section name="RainbowLegacyRule" type="System.Configuration.SingleTagSectionHandler,System,Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089, Custom=null" />
		</sectionGroup>
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler,log4net" />
		<sectionGroup name="providers">
			<section name="log" type="Rainbow.Configuration.Provider.ProviderConfigurationHandler, System.Configuration.Provider" />
			<section name="urlBuilder" type="Rainbow.Configuration.Provider.ProviderConfigurationHandler, System.Configuration.Provider" />
		</sectionGroup>
		<section name="microsoft.web.services2" type="Microsoft.Web.Services2.Configuration.WebServicesConfiguration, Microsoft.Web.Services2, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
	</configSections>
	<!-- Application specific settings -->
	<appSettings file="Rainbow.config">
		<!-- Update this key when you change the Rainbow.config file, day/month/year -->
		<add key="RainbowConfigUpdated" value="01/03/2005" />
		<!-- DB SETTINGS -->
		<!-- DB connection string -->
		<!-- If you upgrade from 1317 you should use SQL authentication -->
		<!-- 
		Changing authentication type across installations is NOT RECOMMENDED
		Particulary: If you update DB using integrated security you should keep using it.
		Or use a script for changing existing owner.
		Get it at: http://www.rainbowportal.net/DesktopModules/Articles/ArticlesView.aspx?Alias=rainbow&TabID=0&ItemID=36&mid=10440
		-->
		<!--
		To have multiple dbs for a single source add it's portalAlias and append _ConnectionString and then fill in the details as normal.
		If Rainbow does not detect your portalAlias here it will use the default Connection String.
		Remember that this has important performance implications.
		So you have to explicity enable it set the EnableMultiDbSupport to true
		-->
		<add key="EnableMultiDbSupport" value="false" />
		<!--WISEMETA: value="server=[SERVER];database=[DESTINATIONDB];uid=[RUNTIMEUSER];pwd=[RUNTIMEPASSWORD];" -->
		<add key="ConnectionString" value="server=localhost;Trusted_Connection=true;database=Rainbow;Application Name=Rainbow" />
		<!--
		<add key="ConnectionString" value="server=localhost;database=Rainbow;uid=sa;pwd=" />
		-->
		<!-- END DB SETTINGS -->
		<!-- Scheduler settings -->
		<add key="SchedulerEnable" value="no" />
		<add key="SchedulerCacheSize" value="100" />
		<add key="SchedulerPeriod" value="60000" />
		<!-- end scheduler settings -->
		<!-- SMTP server -->
		<add key="SmtpServer" value="localhost" />
		<add key="EmailFrom" value="portal@localhost.com" />
		<!-- secure server, SSL enabled -->
		<!--
		use a DNS pointing to www.portal.net/secure or protect the secure directory 
		if you do not have SSL, use http instead of https 
		NOTE: If not present it defaults to ECommerce/Secure dir on current portal
		 -->
		<!--<add key="PortalSecureDirectory" value="http://localhost/rainbow/secure" />-->
		<!--<add key="PortalSecureDirectory" value="https://www.portal.net/secure" />-->
		<!--<add key="PortalSecureDirectory" value="https://secure.portal.net" />-->
		<!-- 
		In case of integrated windows authentication, specify a list of Active Directory Domains here 
		Separate the domains with ';'. The first domain will be selected by default.
		Domains should be in following format: 
		Active directory ( W2K domains ): LDAP://ADcomputer/DCpath 
		NT domains: WinNT://DomainName
		-->
		<add key="ADdns" value="LDAP://DomainControllerName/DC=MyDomain, DC=com; WinNT://MyDomain" />
		<!-- When using the active directory support, the Admins group from 
		     Rainbow doesn't work anymore. Therefore specify the group or user who replaces the rainbow Admins role. -->
		<add key="ADAdministratorGroup" value="MyDomain\Administrators" />
		<!-- 
		// Uncomment these tags to utilise LDAP Support
		<add key="LDAPAdministratorGroup" value="cn=Portal-Admin,ou=Groups,ou=ece,ou=eng,o=rmit"/>
		<add key="LDAPLogin" value="cn=e24445,ou=staff,ou=ece,ou=eng,o=rmit; 2004shona"/>
		<add key="LDAPServer" value="ldap.rmit.edu.au:389"/>
		<add key="LDAPGroup" value="ou=Groups,ou=ece,ou=eng,o=rmit/Objectclass=groupOfNames"/>
		<add key="LDAPContexts" value="ou=staff,ou=ece,ou=eng,o=rmit;ou=pgc,ou=ece,ou=eng,o=rmit;ou=ug,ou=ece,ou=eng,o=rmit"/>
		-->
		<!-- 
		2004-07-28, Leo Duran
		Added to fix a bug that shows up when active directory running on the IIS
		Computer that is running Rainbow.
		
		Set EnableADUser to true if you wish to use this code.
		Add a valid network user name... I create a basic user for this purpose. The user doesn't
		need any special privelages.
		Enter the password that you assigned to the user.
		-->
		<add key="EnableADUser" value="false" />
		<add key="ADUserName" value="MyDomain\UserName" />
		<add key="ADUserPassword" value="password" />
		<!-- 
		Default language, you may override the defaultlanguage.
		This value will be ovverridden by querystring value and
		browser default. Provide it is a valid culture string.
		 -->
		<add key="DefaultLanguage" value="en-US" />
		<!-- Proxy server parameters -->
		<add key="ProxyServer" value="http://127.0.0.1" />
		<add key="ProxyUserID" value="" />
		<add key="ProxyPassword" value="" />
		<add key="ProxyDomain" value="" />
		<!-- Set it to true if your web server is behind a proxy. Used in e.g. module XmlFeed -->
		<add key="UseProxyServerForServerWebRequests" value="false" />
		<!-- key added for changes to XmlFeed module (and other that uses xslt files)
		Leave this commented out unless you want to store the xslt files in an alternate location.
		By default they are in the folder that maps to the application root\DesktopModules\XmlFeed\.
		This setting indicates the folder where xslt files for transforming feeds are located.
		It iss used to populate the dropdown. If you create new .xslt files you can just put them
		in the folder and they will show up in the dropdown.
		
		For security, you may want to store these files outside the web so they can't be retrieved via http.
		Important: You may also want to give the ASP worker process only read permissions to the folder.
		-->
		<!--
		<add key="XMLFeedXSLFolder" value="~/DesktopModules/XmlFeed/" />
		-->
		<!-- 
		This is the alias of the portal the will be shown by default.
		If you run on a domain remember to change portalalias on db!
		Default portal is shown when you call directly default IP
		like:  http://127.0.0.1 or when the host is localhost
		-->
		<add key="DefaultPortal" value="rainbow" />
		<!-- Used to set default portal -->
		<add key="DefaultIP" value="127.0.0.1" />
		<!-- Portal title prefix -->
		<add key="PortalTitlePrefix" value="Rainbow - " />
		<!-- Used for store the differents portals folders -->
		<add key="PortalsDirectory" value="Portals" />
		<!-- 
        If true the last part of domain is truncated: www.rainbowportal.net
        and www.rainbowportal.org will match www.rainbowportal
        Please note that if you change this to false you have to change all portal alias 
        on all portals in portal table to match the complete domain name
        -->
		<add key="IgnoreFirstDomain" value="true" />
		<!-- 
        If true www on start will be removed so you can use:
        alias = demo
        and get www.demo.com and demo.com match the same portal.
        Otherwise will match different portals.
        Please note that if you change this to true you have to change all portal alias 
        on all portals in portal table to match the domain name whitout www
        -->
		<add key="RemoveWWW" value="true" />
		<!-- 
        Portal setting XML support.
        You can enable disable here.
        Heep it disable to gain performance if you do not use it.
		-->
		<add key="PortalSettingDesktopTabsXml" value="false" />
		<!-- Default theme name (dir) -->
		<add key="DefaultTheme" value="Default" />
		<!-- Single User base -->
		<!-- 
		Use this setting to authenticate users against one single user base
		All authentication requests will be authenticated using the portal 0 user base.
		Change this before adding new portals. Switch from true / false 
		its is not recommended because no change occurs on db. 
		If you change it you must manually ensure database consistency.
		 -->
		<add key="UseSingleUserBase" value="false" />
		<!-- Enable/disable monitoring using this switch -->
		<add key="EnableMonitoring" value="true" />
		
		<!-- Amazon Module defaults -->
		<add key="AmazonPromoCode" value="wwwrainbowpor-20" />
		<add key="AmazonDevToken" value="" />
	</appSettings>
	<system.web>
		<browserCaps>
			<!-- 
			Name:		BrowserCaps update for modern browsers, http://slingfive.com/pages/code/browserCaps/
			Author:		Rob Eberhardt, http://slingfive.com/
			History:
				2004-11-19	improved detection of Safari, Konqueror &amp; Mozilla variants, added Opera detection
				2003-12-21	updated TagWriter info
				2003-12-03	first published
			-->

			<!-- GECKO Based Browsers (Netscape 6+, Mozilla/Firefox, ...) //-->
			<case match="^Mozilla/5\.0 \([^)]*\) (Gecko/[-\d]+)(?'VendorProductToken' (?'type'[^/\d]*)([\d]*)/(?'version'(?'major'\d+)(?'minor'\.\d+)(?'letters'\w*)))?">
				browser=Gecko
				<filter>
					<case match="(Gecko/[-\d]+)(?'VendorProductToken' (?'type'[^/\d]*)([\d]*)/(?'version'(?'major'\d+)(?'minor'\.\d+)(?'letters'\w*)))">
						type=${type}
					</case>
					<case> 
						<!-- plain Mozilla if no VendorProductToken found -->
						type=Mozilla
					</case>
				</filter>
				frames=true
				tables=true
				cookies=true
				javascript=true
				javaapplets=true
				ecmascriptversion=1.5
				w3cdomversion=1.0
				css1=true
				css2=true
				xml=true
				tagwriter=System.Web.UI.HtmlTextWriter
				<case match="rv:(?'version'(?'major'\d+)(?'minor'\.\d+)(?'letters'\w*))">
					version=${version}
					majorversion=0${major}
					minorversion=0${minor}
					<case match="^b" with="${letters}">
						beta=true
					</case>
				</case>
			</case>

			<!-- AppleWebKit Based Browsers (Safari...) //-->
			<case match="AppleWebKit/(?'version'(?'major'\d?)(?'minor'\d{2})(?'letters'\w*)?)">
				browser=AppleWebKit
				version=${version}
				majorversion=0${major}
				minorversion=0.${minor}
				frames=true
				tables=true
				cookies=true
				javascript=true
				javaapplets=true
				ecmascriptversion=1.5
				w3cdomversion=1.0
				css1=true
				css2=true
				xml=true
				tagwriter=System.Web.UI.HtmlTextWriter
				<case match="AppleWebKit/(?'version'(?'major'\d)(?'minor'\d+)(?'letters'\w*))(.* )?(?'type'[^/\d]*)/.*( |$)">
					type=${type}
				</case>
			</case>

			<!-- Konqueror //-->
			<case match=".+[K|k]onqueror/(?'version'(?'major'\d+)(?'minor'(\.[\d])*)(?'letters'[^;]*));\s+(?'platform'[^;\)]*)(;|\))">
				browser=Konqueror
				version=${version}
				majorversion=0${major}
				minorversion=0${minor}
				platform=${platform}
				type=Konqueror
				frames=true
				tables=true
				cookies=true
				javascript=true
				javaapplets=true
				ecmascriptversion=1.5
				w3cdomversion=1.0
				css1=true
				css2=true
				xml=true
				tagwriter=System.Web.UI.HtmlTextWriter
			</case>

			<!-- Opera //-->
			<case match="Opera[ /](?'version'(?'major'\d+)(?'minor'\.(?'minorint'\d+))(?'letters'\w*))">
				<filter match="[7-9]" with="${major}">
					tagwriter=System.Web.UI.HtmlTextWriter
				</filter>
				<filter>
					<case match="7" with="${major}">
						<filter>
							<case match="[5-9]" with="${minorint}">
								ecmascriptversion=1.5
							</case>
							<case>
								ecmascriptversion=1.4
							</case>
						</filter>
					</case>
					<case match="[8-9]" with="${major}">
						ecmascriptversion=1.5
					</case>
				</filter>
			</case>
		</browserCaps>

		<!--  GLOBALIZATION
          This section sets the globalization settings of the application. 
          Utf-8 is not supported on Netscape 4.x 
          If you need netscape compatiblity leave iso-8859-1.
          UTF-8 is recommended for complex languages
          ** Jes1111 - added culture, uiCulture and fileEncoding
		-->
		<globalization culture="en-US" uiCulture="en" requestEncoding="UTF-8" responseEncoding="UTF-8"
			fileEncoding="UTF-8" />
		<!--<globalization culture="en-US" uiCulture="en"  fileEncoding="iso-8859-1" requestEncoding="iso-8859-1" responseEncoding="iso-8859-1"/>-->
		<!-- Framework 1.1 security check -->
		<!-- Uncomment for Framework 1.1 -->
		<!-- Comment for Framework 1.0 -->
		<pages validateRequest="false" />
		<!---->
		<!-- HttpURLHandler for handling url requests -->
		<httpModules>
			<add type="Rewrite.NET.Rewrite,Rewrite.NET" name="Rewrite.NET" />
			<!--<add name="XHTMLHTTPModule" type="AspNetResources.Web.XHTMLHTTPModule, XHTMLHTTPModule" />-->
		</httpModules>
		<!-- Session state -->
		<sessionState mode="InProc" cookieless="false" timeout="30" stateConnectionString="tcpip=localhost:42424"
			sqlConnectionString="data source=localhost;Integrated Security=SSPI;Initial Catalog=Rainbow" />
		<!-- set debugmode to false for running application -->
		<compilation debug="true" />
		<!--
		Rainbow Portal supports either Forms authentication (Internet)
        or Windows authentication (for intranets).  Forms Authentication is
        the default.  To change to Windows authentication, comment the 
        <authentication mode="Forms"> section below, and uncomment the 
        <authentication mode="Windows"> section. 
		-->
		<authentication mode="Forms">
			<forms name=".ASPXAUTH" protection="All" timeout="60" />
		</authentication>
		<!--	       
        <authentication mode="Windows" />
        <authorization>
            <deny users="?" />
        </authorization>
		-->
		<!-- Set here max upload size -->
		<httpRuntime useFullyQualifiedRedirectUrl="true" maxRequestLength="4096" />
		<!-- Complements Rainbow Support Functionality - See Rainbow.config -->
		<!--
		There are three different modes:
		  "On" Always display custom (friendly) messages  
          "Off" Always display detailed ASP.NET error information.
          "RemoteOnly" Display custom (friendly) messages only to users not running 
          on the local Web server. This setting is recommended for security purposes, so 
          that you do not display application detail information to remote clients.
		
		  To enable dynamic error pages, change the extension from .html to .aspx but in case 
		  of error on the error page (e.g the original error was denied access to database so 
		  when the dynamic page tries to load theme etc it to encounters an error)it does not 
		  redirect to the html equivalent on all servers/errors
		  so please test before deploying. A good test is to switch off the db test server. This will
		  cause the dynamic error pages to fail.
		-->
		<customErrors defaultRedirect="~/app_support/GeneralError.html" mode="RemoteOnly">
			<error statusCode="404" redirect="~/app_support/Error404.html" />
			<error statusCode="403" redirect="~/app_support/Error403.html" />
		</customErrors>
		<httpHandlers>
			<add verb="GET" path="FtbWebResource.axd" type="FreeTextBoxControls.AssemblyResourceHandler, FreeTextBox" />
			<add path="UploadDialog.aspx" verb="*" type="StaticDust.Web.UploadHandler, StaticDust.Web.UI.Controls.UploadDialog"
				validate="false" />
		</httpHandlers>
		<webServices>
			<soapExtensionTypes>
				<add type="Microsoft.Web.Services2.WebServicesExtension, Microsoft.Web.Services2, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" priority="1" group="0" />
			</soapExtensionTypes>
		</webServices>
	</system.web>
	<Esperantus>
		<!-- 
		The autolearn features fills up the database
		with missing localized keys and updates usage.
		Suggestion: turn off on production sites.
		Very useful for module creators and translators.
		-->
		<add key="AutoLearn" value="False" />
		<!-- 
		If true empty keys will not be saved
		 -->
		<add key="AutoLearnIgnoreEmptyKeys" value="True" />
		<!-- 
		If true no error is throw if there are problems on saving keys
		 -->
		<add key="AutoLearnIgnoreSaveErrors" value="True" />
		<!-- 
		Convert Keys to Upper case. Treat all keys as it would be all uppercase.
		Keys are case sensitive
		 -->
		<add key="ConvertToUpperCase" value="True" />
		<!-- 
		Set here the default store and parameters. Case sensitive.
		Valid CountryRegionsStore values are: Resources and OledDb
		Default value: Resources

		Parameters for Resources:
		AssemblyName=<assemblyname>;CountriesSubStore=<Resourcestream>
		Defaults: AssemblyName=Esperantus.dll;CountriesSubStore=Esperantus.Resources.Countries.xml
		
		Parameters for OleDB: Any valid OleDB connection string
		
		You could uncomment below keys and provide new values.
		Default values are set on Application_Start method in Global.asax.cs
		-->
		<!--
		<add key="CountryRegionsStore" value="OleDB" />
		<add key="CountryRegionsStoreParameters" value="Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Rainbow;Data Source=localhost;" />
		-->
		<!-- 
		Set here the default store and parameters. Case sensitive.
		Valid KeysStore values are: 
		-Resources (Default value)
		-ResourcesPA (private assemblies support)
		-StoreXMLResxFiles
		-Hybrid
		-OledDb

		*********************************
		Parameters for Resources (readonly):
		AssemblyName=<assemblynamefullpath>;KeysSubStore=<Resourcestream>
		Defaults:
		AssemblyName=Esperantus.dll;KeysSubStore=""
		Sample:
		AssemblyName=C:\inetpub\EsperantusWeb\bin\Esperantus.dll;KeysSubStore="mylang"
		
		*********************************
		Parameters for ResourcesPA (readonly):
		AssemblyName=<assemblynamefullpath>,<assemblynamefullpath>;KeysSubStore=<Resourcestream>,<Resourcestream>
		Defaults:
		AssemblyName=Esperantus.dll;KeysSubStore=""
		Sample:
		AssemblyName=C:\inetpub\EsperantusWeb\bin\Esperantus.dll,C:\inetpub\EsperantusWeb\bin\MyPA.dll;KeysSubStore="mylang","myPa"
		
		*********************************
		Parameters for OleDB (read/write, can use autolearn): Any valid OleDB connection string
		
		*********************************
		Parameters for StoreXMLResxFiles (read/write, can use autolearn):
		Path=<The folder that contians the files>;
		FilesPrefix=<Strings>; Only files that start with specified string will be considered.
		This let you kave different resources sets in the same folder.
		
		*********************************
		Parameters for Hybrid (read/write, can use autolearn):
		Hybrid combines a Resources store (for read) and a StoreXMLResxFiles (for write).
		You have to specify all values for both stores:
		AssemblyName;KeysSubStore;Path;FilesPrefix
		
		*********************************
		You could uncomment below keys and provide new values.
		Default values are set on Application_Start method in Global.asax.cs
		-->
		<!--
		<add key="KeysStore" value="Hybrid" />
		<add key="KeysStoreParameters" value="AssemblyName=C:\Dev\CVSROOT\Rainbow\bin\Rainbow.dll;KeysSubStore=Rainbow.Resources.Rainbow;Path=C:\Dev\CVSROOT\Rainbow\Resources;FilesPrefix=Rainbow" />
		-->
		<!--
		<add key="KeysStore" value="ResourcesPA" />
		<add key="KeysStoreParameters" value="AssemblyName=C:\web\Booking2005\Rainbow.dll,C:\web\Booking2005\Rainbow.Ecommerce.Booking.dll;KeysSubStore=Rainbow.Resources.Rainbow,Rainbow.Resources.Ecommerce.Booking.it" />
		-->
	</Esperantus>
	<BankGateways>
		<!-- key must match the Name property of the gateway.  value="FullClass with namespace,Assembly" -->
		<add key="CreditTransfer" value="Rainbow.ECommerce.Gateways.GatewayCreditTransfer,DUEMETRI.Rainbow.ECommerce.BankGateway.dll" />

		<!--
		<add key="Carige" value="Rainbow.ECommerce.Gateways.GatewayCarige,DUEMETRI.Rainbow.ECommerce.GatewayItalianBanks.dll" />
		<add key="Telepay" value="Rainbow.ECommerce.Gateways.GatewayTelepay,DUEMETRI.Rainbow.ECommerce.GatewayItalianBanks.dll" />
		<add key="SellaCripto" value="Rainbow.ECommerce.Gateways.GatewaySellaCripto,DUEMETRI.Rainbow.ECommerce.GatewayItalianBanks.dll" />
		<add key="BankPass" value="Rainbow.ECommerce.Gateways.GatewayBankPass,DUEMETRI.Rainbow.Ecommerce.GatewayItalianBanks.dll" />
		-->
	</BankGateways>
	<ShippingObjects>
		<!-- key must match the Name property of the Shipping Object. value="FullClass with namespace,Assembly" -->
		<add key="FixedShipping" value="Rainbow.ECommerce.ShippingFixed,Rainbow.ECommerce.dll" />
		<add key="ElectronicDelivery" value="Rainbow.ECommerce.ShippingElectronicDelivery,Rainbow.ECommerce.dll" />
		<add key="SimpleShipping" value="Rainbow.ECommerce.ShippingSimple,Rainbow.ECommerce.dll" />
	</ShippingObjects>
	<log4net>
		<logger name="Rainbow">
			<level value="ALL" />
			<appender-ref ref="RollingFile" />
			<!--<appender-ref ref="SqlNetAppender" /> -->
		</logger>
		<!-- Create db table before using it -->
		<!-- http://log4net.sourceforge.net/release/1.2.0.30507/doc/manual/example-config-appender.html -->
		<appender name="RollingFile" type="log4net.Appender.RollingFileAppender">
			<file value="rb_logs/rb_log" />
			<appendToFile value="true" />
			<rollingMode value="Date" />
			<layout type="log4net.Layout.PatternLayout">
				<conversionPattern value="%d %-4r [%t] %-5p %c %x - %m%n" />
			</layout>
		</appender>
		<appender name="SqlNetAppender" type="log4net.Appender.ADONetAppender">
			<param name="BufferSize" value="100" />
			<param name="ConnectionType" value="System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
			<param name="ConnectionString" value="data source=localhost;initial catalog=Rainbow;integrated security=false;persist security info=True;User ID=sa;Password=" />
			<param name="CommandText" value="INSERT INTO rb_Log ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception)" />
			<param name="Parameter">
				<param name="ParameterName" value="@log_date" />
				<param name="DbType" value="DateTime" />
				<param name="Layout" type="log4net.Layout.RawTimeStampLayout" />
			</param>
			<param name="Parameter">
				<param name="ParameterName" value="@thread" />
				<param name="DbType" value="String" />
				<param name="Size" value="255" />
				<param name="Layout" type="log4net.Layout.PatternLayout">
					<param name="ConversionPattern" value="%t" />
				</param>
			</param>
			<param name="Parameter">
				<param name="ParameterName" value="@log_level" />
				<param name="DbType" value="String" />
				<param name="Size" value="50" />
				<param name="Layout" type="log4net.Layout.PatternLayout">
					<param name="ConversionPattern" value="%p" />
				</param>
			</param>
			<param name="Parameter">
				<param name="ParameterName" value="@logger" />
				<param name="DbType" value="String" />
				<param name="Size" value="255" />
				<param name="Layout" type="log4net.Layout.PatternLayout">
					<param name="ConversionPattern" value="%c" />
				</param>
			</param>
			<param name="Parameter">
				<param name="ParameterName" value="@message" />
				<param name="DbType" value="String" />
				<param name="Size" value="4000" />
				<param name="Layout" type="log4net.Layout.PatternLayout">
					<param name="ConversionPattern" value="%m" />
				</param>
			</param>
			<param name="Parameter">
				<param name="ParameterName" value="@exception" />
				<param name="DbType" value="String" />
				<param name="Size" value="2000" />
				<param name="Layout" type="log4net.Layout.ExceptionLayout" />
			</param>
		</appender>
	</log4net>
	<!--
	Name conventions for provider class:
	Specific type + Provider Type + "Provider" keyword
	eg: AccessLogProvider or SqlLogProvider or Log4NetLogProvider
	
	Namespace conventions:
	Namespace Extension as usual, no special namespace for providers, group by functionalities
	(eg: Rainbow.Configuration or Rainbow.Web or Duemetri.Configuration)
	
	Friendly Name conventions:
	Specific type + Provider Type, remove "Provider" keyword
	eg: AccessLog or SqlLog or Log4NetLog
	-->
	<providers>
		<log defaultProvider="Log4NetLog">
			<providers>
				<clear />
				<add name="Log4NetLog" type="Rainbow.Configuration.Log4NetLogProvider, Rainbow.Provider.Implementation" />
			</providers>
		</log>
		<urlBuilder defaultProvider="SqlUrlBuilder">
			<providers>
				<clear />
				<add name="SqlUrlBuilder" type="Rainbow.Web.SqlUrlBuilderProvider, Rainbow.Provider.Implementation"
					handlerflag="site" handlersplitter="__" pageidnosplitter="true" friendlypagename="default.aspx"
					aliasinurl="false" langinurl="false" cacheminutes="5" ignoretargetpage="tablayout.aspx" />
			</providers>
		</urlBuilder>
	</providers>
	<Rewrite.NET>
		<Index>
			<!--
			Format:
				<add key="SECTION NAME" value="NAMESPACE.CLASSNAME,ASSEMBLY NAME" />
			Example:
				<add key="RainbowDefaultRule" value="RewriteRules.Rainbow.DefaultRule,RewriteRules.Rainbow" />
			-->
			<!-- Comment out if you do not have the RewriteRules.Rainbow Assembly in the bin directory-->
			<add key="RainbowDefaultRule" value="RewriteRules.Rainbow.DefaultRule,RewriteRules.Rainbow" />
			<add key="RainbowLegacyRule" value="RewriteRules.Rainbow.DefaultRule,RewriteRules.Rainbow" />
		</Index>
		<!--This setting can either be Cumulative or BreakOnFirst (Case sensitive) to tell the engine to either go through all the rules or break on the first match-->
		<Settings EngineType="BreakOnFirst" />
		<!-- Comment out if you do not have the RewriteRules.Rainbow Assembly in the bin directory-->
		<RainbowDefaultRule handlerflag="site" handlersplitter="__" pageidnosplitter="true" />
		<RainbowLegacyRule handlerflag="portal" handlersplitter="__" pageidnosplitter="false" />
	</Rewrite.NET>
	<!--
	
			Rewrite.Net & UrlBuilder Notes
			
			You can have many different rules added. The default ones may have the following settings:

			*	handlerflag (must be implemented by rules)  
			
				The name used by the rewrite rule and the urlbuilder rule so they can 
				identify each other (i.e. urlbuilder builds a url containing this flag, rewrite rule 
				sees this flag and loads up appropriate rule to parse it)

			*	handlersplitter (must be implemented by rules [apart from legacy rule which is just there to understand old urls]  
			
				This is a flag used to distinguish between name and value e.g. tabid__5 in a url becomes tabid=5 (if you url 
				builder is appending these values as normal e.g. ?tabid=5 then simply set this flag as = for your url builder rule.
				
				Possible Keys = "-" | "_" | "." | "!" | "~" | "*" | "'" | "(" | ")" I do not recommend using single _ - or . as they may be used by filenames or querystring names/values e.g. lang=en-GB
			
			*	aliasinurl 
			
				Should the domain's alias show up somewhere in the url  

				Microsoft Urlscan Security Tool note:
				Alias with a dot inside are filtered. 
				Set this value to false if you use Urlscan and dotted aliases.

			*	langinurl
			
				Should the language id show up in the url  
			
				The name used by the rewrite rule and the urlbuilder rule so they can 
				identify each other (i.e. urlbuilder builds a url containing this flag, rewrite rule 
				sees this flag and loads up appropriate rule to parse it)
				
				Other settings will be described in documentation.
				
				Please note that the "HandlerTargetUrl" setting in Rainbow.config is used by all rules.
	 -->
	<microsoft.web.services2>
		<diagnostics />
	</microsoft.web.services2>
</configuration>
