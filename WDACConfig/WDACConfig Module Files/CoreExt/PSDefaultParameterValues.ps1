# $PSDefaultParameterValues only get read from scope where invocation occurs
# This is why this file is dot-sourced in every other component of the WDACConfig module at the beginning
# Main cmdlets that are also used within other main cmdlets are mentioned here too.
$PSDefaultParameterValues = @{
    'Invoke-WebRequest:HttpVersion'                               = '3.0'
    'Invoke-WebRequest:SslProtocol'                               = 'Tls12,Tls13'
    'Invoke-RestMethod:HttpVersion'                               = '3.0'
    'Invoke-RestMethod:SslProtocol'                               = 'Tls12,Tls13'
    'Import-Module:Verbose'                                       = $false
    'Remove-Module:Verbose'                                       = $false
    'Export-ModuleMember:Verbose'                                 = $false
    'Add-Type:Verbose'                                            = $false
    'Get-WinEvent:Verbose'                                        = $false
    'Test-Path:ErrorAction'                                       = 'SilentlyContinue'
    'Receive-CodeIntegrityLogs:Verbose'                           = $Verbose
    'Get-SignTool:Verbose'                                        = $Verbose
    'Update-Self:Verbose'                                         = $Verbose
    'New-SnapBackGuarantee:Verbose'                               = $Verbose
    'Get-KernelModeDriversAudit:Verbose'                          = $Verbose
    'Get-SignerInfo:Verbose'                                      = $Verbose
    'Get-CertificateDetails:Verbose'                              = $Verbose
    'Compare-SignerAndCertificate:Verbose'                        = $Verbose
    'Compare-SignerAndCertificate:Debug'                          = $Debug
    'Remove-SupplementalSigners:Verbose'                          = $Verbose
    'Set-LogPropertiesVisibility:Verbose'                         = $Verbose
    'Test-KernelProtectedFiles:Verbose'                           = $Verbose
    'Set-CiRuleOptions:Verbose'                                   = $Verbose
    'New-WDACConfig:Verbose'                                      = $Verbose
    'Get-KernelModeDrivers:Verbose'                               = $Verbose
    'New-Macros:Verbose'                                          = $Verbose
    'Checkpoint-Macros:Verbose'                                   = $Verbose
    'Test-ECCSignedFiles:Verbose'                                 = $Verbose

    'Clear-CiPolicy_Semantic:Verbose'                             = $Verbose
    'Close-EmptyXmlNodes_Semantic:Verbose'                        = $Verbose
    'Compare-CorrelatedData:Verbose'                              = $Verbose
    'Merge-Signers_Semantic:Verbose'                              = $Verbose
    'New-FilePublisherLevelRules:Verbose'                         = $Verbose
    'New-HashLevelRules:Verbose'                                  = $Verbose
    'New-PublisherLevelRules:Verbose'                             = $Verbose
    'Optimize-MDECSVData:Verbose'                                 = $Verbose
    'Remove-AllowElements_Semantic:Verbose'                       = $Verbose
    'Remove-DuplicateFileAttrib_Semantic:Verbose'                 = $Verbose
    'Remove-UnreferencedFileRuleRefs:Verbose'                     = $Verbose
    'New-CertificateSignerRules:Verbose'                          = $Verbose

    'Clear-CiPolicy_Semantic:Debug'                               = $Debug
    'Close-EmptyXmlNodes_Semantic:Debug'                          = $Debug
    'Compare-CorrelatedData:Debug'                                = $Debug
    'Merge-Signers_Semantic:Debug'                                = $Debug
    'New-FilePublisherLevelRules:Debug'                           = $Debug
    'New-HashLevelRules:Debug'                                    = $Debug
    'New-PublisherLevelRules:Debug'                               = $Debug
    'Optimize-MDECSVData:Debug'                                   = $Debug
    'Remove-AllowElements_Semantic:Debug'                         = $Debug
    'Remove-DuplicateFileAttrib_Semantic:Debug'                   = $Debug
    'Remove-UnreferencedFileRuleRefs:Debug'                       = $Debug
    'New-CertificateSignerRules:Debug'                            = $Debug
}
