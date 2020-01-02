<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:rainbow="urn:rainbow">
	<xsl:output method="xml" version="1.0" indent="no"/>
	<xsl:strip-space elements="*"/>
	<xsl:param name="ClientScriptLocation"/>
	<xsl:param name="Orientation"/>
	<xsl:param name="ActiveTabId">1</xsl:param>
	<xsl:param name="ContainerCssClass"/>
	<!--	<xsl:variable name="myBranch" select="/MenuData/MenuGroup/MenuItem[descendant-or-self::MenuItem[@ID = $ActiveTabId]]"/>-->
	<xsl:variable name="myBranch" select="//MenuItem[@ParentTabId = '0']"/>
	<xsl:variable name="sectionName" select="$myBranch/@Label"/>
	<xsl:template match="/">
		<xsl:element name="div">
			<xsl:attribute name="class"><xsl:value-of select="$ContainerCssClass"/></xsl:attribute>
			<ul class="zen-menu">
				<xsl:apply-templates select="MenuData/MenuGroup"/>
			</ul>
		</xsl:element>
	</xsl:template>
	<xsl:template match="MenuGroup">
		<ul>
			<xsl:apply-templates/>
		</ul>
	</xsl:template>
	<xsl:template match="MenuItem">
		<xsl:choose>
			<xsl:when test="rainbow:CheckRoles(string(@AuthRoles))">
				<xsl:element name="li">
					<xsl:element name="a">
						<xsl:attribute name="href"><xsl:value-of select="rainbow:BuildUrl(string(@Label),number(@ID))"/></xsl:attribute>
						<xsl:choose>
							<xsl:when test="@ID=$ActiveTabId">
								<xsl:attribute name="class"><xsl:text>MenuItemSelected</xsl:text></xsl:attribute>
							</xsl:when>
							<xsl:when test=".//MenuItem">
								<xsl:choose>
									<xsl:when test=".//MenuItem[@ID=$ActiveTabId]">
										<xsl:attribute name="class"><xsl:text>daddy MenuItemSelected</xsl:text></xsl:attribute>
									</xsl:when>
									<xsl:otherwise>
										<xsl:attribute name="class"><xsl:text>daddy</xsl:text></xsl:attribute>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:when>
						</xsl:choose>
						<xsl:value-of select="@Label"/>
					</xsl:element>
					<xsl:apply-templates/>
				</xsl:element>
			</xsl:when>
		</xsl:choose>
	</xsl:template>
</xsl:stylesheet>
