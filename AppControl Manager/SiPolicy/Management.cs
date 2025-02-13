﻿using System;
using System.Xml;
using AppControlManager.Main;
using AppControlManager.XMLOps;

namespace AppControlManager.SiPolicy;

internal static class Management
{

	/// <summary>
	/// This class uses the auto-generated code from the XSD schema to initialize the SiPolicy object
	/// By accepting a string path to a valid XML file
	/// Native AOT/Trimming compatible
	///
	/// Generated by the following command:
	/// . "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\x64\xsd.exe" "C:\Windows\schemas\CodeIntegrity\cipolicy.xsd" /classes /namespace:AppControlManager.SiPolicy /language:CS
	/// </summary>
	/// <param name="xmlFilePath"></param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	internal static SiPolicy Initialize(string xmlFilePath)
	{
		if (!CiPolicyTest.TestCiPolicy(xmlFilePath))
		{
			throw new InvalidOperationException($"The XML file '{xmlFilePath}' is not compliant with the CI policy schema");
		}

		/*

		XmlSerializer serializer = new(typeof(SiPolicy));

		// Use XmlReader with secure settings
		XmlReaderSettings settings = new()
		{
			DtdProcessing = DtdProcessing.Prohibit, // Disable DTD processing
			XmlResolver = null,                     // Prevent external entity resolution
			Async = true // Async
		};

		using XmlReader reader = XmlReader.Create(xmlFilePath, settings);

		if (serializer.Deserialize(reader) is not SiPolicy policy)
		{
			throw new InvalidOperationException($"Could not instantiate the XML file '{xmlFilePath}'");
		}

		return policy;

		*/

		return CustomDeserialization.DeserializeSiPolicy(xmlFilePath);
	}


	/// <summary>
	/// Saves the SiPolicy object to a XML file.
	/// Uses custom hand made serialization logic that is compatible with Native AOT compilation
	/// </summary>
	/// <param name="policy"></param>
	/// <param name="filePath"></param>
	internal static void SavePolicyToFile(SiPolicy policy, string filePath)
	{

		XmlDocument xmlObj = CustomSerialization.CreateXmlFromSiPolicy(policy);

		xmlObj.Save(filePath);

		CloseEmptyXmlNodesSemantic.Close(filePath);

		/*

		XmlSerializer serializer = new(typeof(SiPolicy));

		// Create XmlSerializerNamespaces to include only the desired namespace
		XmlSerializerNamespaces namespaces = new();
		namespaces.Add(string.Empty, GlobalVars.SiPolicyNamespace); // Default namespace without prefix

		XmlWriterSettings settings = new()
		{
			Indent = true,                // Format the XML for readability
			NewLineOnAttributes = false,  // Keep attributes on the same line
			Async = true,                 // Async support for better performance with large files
			OmitXmlDeclaration = false,   // Include the XML declaration
			Encoding = System.Text.Encoding.UTF8 // Ensure UTF-8 encoding
		};

		using XmlWriter writer = XmlWriter.Create(filePath, settings);
		serializer.Serialize(writer, policy, namespaces);

		*/

		if (!CiPolicyTest.TestCiPolicy(filePath))
		{
			throw new InvalidOperationException($"The XML file '{filePath}' created at the end is not compliant with the CI policy schema");
		}

	}

}
