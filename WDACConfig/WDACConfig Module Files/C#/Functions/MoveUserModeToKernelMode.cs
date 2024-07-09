using System;
using System.Linq.Expressions;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace WDACConfig
{
    public static class MoveUserModeToKernelMode
    {
        // Moves all User mode AllowedSigners in the User mode signing scenario to the Kernel mode signing scenario and then
        // deletes the entire User mode signing scenario block
        // This is used during the creation of Strict Kernel-mode WDAC policy for complete BYOVD protection scenario.
        // It doesn't consider <FileRulesRef> node in the SigningScenario 12 when deleting it because for kernel-mode policy everything is signed and we don't deal with unsigned files.
        public static void Move(string filePath)
        {
            try
            {
                // Create an XmlDocument object
                XmlDocument xml = new XmlDocument();

                // Load the XML file
                xml.Load(filePath);

                // Create an XmlNameSpaceManager object
                XmlNamespaceManager nsManager = new XmlNamespaceManager(xml.NameTable);
                // Define namespace
                nsManager.AddNamespace("sip", "urn:schemas-microsoft-com:sipolicy");

                // Get all SigningScenario nodes in the XML file
                XmlNodeList signingScenarios = xml.SelectNodes("//sip:SigningScenario", nsManager);

                // Variables to store SigningScenario nodes with specific values 12 and 131
                XmlNode signingScenario12 = null;
                XmlNode signingScenario131 = null;

                // Find SigningScenario nodes with Value 12 and 131
                foreach (XmlNode signingScenario in signingScenarios)
                {
                    string valueAttr = signingScenario.Attributes["Value"].Value;
                    if (valueAttr == "12")
                    {
                        signingScenario12 = signingScenario;
                    }
                    else if (valueAttr == "131")
                    {
                        signingScenario131 = signingScenario;
                    }
                }

                // If both SigningScenario nodes were found
                if (signingScenario12 != null && signingScenario131 != null)
                {
                    // Get AllowedSigners from SigningScenario with Value 12
                    XmlNode allowedSigners12 = signingScenario12.SelectSingleNode("./sip:ProductSigners/sip:AllowedSigners", nsManager);

                    // If AllowedSigners node exists in SigningScenario 12 and has child nodes
                    if (allowedSigners12 != null && allowedSigners12.HasChildNodes)
                    {
                        // Loop through each child node of AllowedSigners in SigningScenario 12
                        foreach (XmlNode allowedSignerNode in allowedSigners12.ChildNodes)
                        {
                            // Ensure we're only working with XmlElement nodes and not comments or anything else

                            // This line is a pattern matching statement:
                            // allowedSignerNode is the current node from the foreach loop.
                            // The is keyword checks if allowedSignerNode is of type XmlElement.
                            // If the check is successful, allowedSigner is created as a new variable within the scope of the if block, and it is assigned the value of allowedSignerNode.
                            // Essentially, allowedSigner is created implicitly as part of the pattern matching expression.

                            if (allowedSignerNode is XmlElement allowedSigner)
                            {
                                // Create a new AllowedSigner node
                                XmlNode newAllowedSigner = xml.CreateElement("AllowedSigner", "urn:schemas-microsoft-com:sipolicy");

                                // Create a SignerId attribute for the new AllowedSigner node
                                XmlAttribute newSignerIdAttr = xml.CreateAttribute("SignerId");

                                // Set the value of the new SignerId attribute to the value of the existing SignerId attribute
                                newSignerIdAttr.Value = allowedSigner.Attributes["SignerId"].Value;

                                // Append the new SignerId attribute to the new AllowedSigner node
                                newAllowedSigner.Attributes.Append(newSignerIdAttr);

                                // Find the AllowedSigners node in SigningScenario 131
                                XmlNode allowedSigners131 = signingScenario131.SelectSingleNode("./sip:ProductSigners/sip:AllowedSigners", nsManager);

                                // If the AllowedSigners node exists in SigningScenario 131
                                if (allowedSigners131 != null)
                                {
                                    // Append the new AllowedSigner node to the AllowedSigners node in SigningScenario 131
                                    allowedSigners131.AppendChild(newAllowedSigner);
                                }
                            }
                        }

                        // Remove SigningScenario with Value 12 completely after moving all of its AllowedSigners to SigningScenario with the value of 131
                        signingScenario12.ParentNode.RemoveChild(signingScenario12);
                    }
                }

                // Save the modified XML document back to the file
                xml.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }
    }
}
