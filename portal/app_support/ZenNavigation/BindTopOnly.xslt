<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rainbow="urn:rainbow">
	<xsl:output method="html" version="4.0" indent="no"/>
	<xsl:strip-space elements="*"/>
	<xsl:param name="ClientScriptLocation"/>
	<xsl:param name="ActiveTabId"/>
	<xsl:param name="Orientation"/>
	<xsl:param name="ContainerCssClass"/>
	<xsl:template match="/">
		<xsl:element name="div">
			<xsl:attribute name="class"><xsl:value-of select="$ContainerCssClass"/></xsl:attribute>
			<ul>
				<xsl:apply-templates select="MenuData/MenuGroup"/>
			</ul>
		</xsl:element>
	</xsl:template>
	<xsl:template match="MenuItem[@ParentTabId='0']">
		<xsl:choose>
			<xsl:when test="rainbow:CheckRoles(string(@AuthRoles))">
				<xsl:element name="li">
						<xsl:choose>
							<xsl:when test="descendant-or-self::MenuItem[@ID=$ActiveTabId]">
								<xsl:attribute name="class"><xsl:text>MenuItemSelected</xsl:text></xsl:attribute>
							</xsl:when>
						</xsl:choose>
					<xsl:element name="a">
						<xsl:attribute name="href"><xsl:value-of select="rainbow:BuildUrl(string(@Label),number(@ID))"/></xsl:attribute>
						<xsl:choose>
							<xsl:when test="descendant-or-self::MenuItem[@ID=$ActiveTabId]">
								<xsl:attribute name="class"><xsl:text>MenuItemSelected</xsl:text></xsl:attribute>
							</xsl:when>
						</xsl:choose>
						<xsl:value-of select="@Label"/>
					</xsl:element>
				</xsl:element>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
