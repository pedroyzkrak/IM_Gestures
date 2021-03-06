param($installPath, $toolsPath, $package, $project)

function MarkDirectoryAsCopyToOutputRecursive($item)
{
    $item.ProjectItems | ForEach-Object { MarkFileASCopyToOutputDirectory($_) }
}

function MarkFileASCopyToOutputDirectory($item)
{
    Try
    {
        Write-Host Try set $item.Name
        $item.Properties.Item("CopyToOutputDirectory").Value = 2
    }
    Catch
    {
        Write-Host RecurseOn $item.Name
        MarkDirectoryAsCopyToOutputRecursive($item)
    }
}

#Now mark everything in the a directory as "Copy to newer"
MarkDirectoryAsCopyToOutputRecursive($project.ProjectItems.Item("vgbtechs"))

# SIG # Begin signature block
# MIIoHQYJKoZIhvcNAQcCoIIoDjCCKAoCAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCDTYkgw0EU1bvG0
# fKQ8hux6GpK3QErf3Kl+RPA7fxQQhKCCEa8wggT8MIID5KADAgECAhMzAAAAFwOr
# wJCeT/v8AAAAAAAXMA0GCSqGSIb3DQEBCwUAMIGHMQswCQYDVQQGEwJVUzETMBEG
# A1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWlj
# cm9zb2Z0IENvcnBvcmF0aW9uMTEwLwYDVQQDEyhNaWNyb3NvZnQgTWFya2V0cGxh
# Y2UgUHJvZHVjdGlvbiBDQSAyMDExMB4XDTE0MTAwMTE4NDk0N1oXDTE2MDEwMTE4
# NDk0N1owdDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNV
# BAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEeMBwG
# A1UEAxMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMIIBIjANBgkqhkiG9w0BAQEFAAOC
# AQ8AMIIBCgKCAQEAxMbNImP/Gt276rUmfSJVdTJeyUccVGDVccl+vhzKJD5kmRuI
# eErWrb0PDWvoy9jAY8Mbsrd1Gi17y1fDdtcrclFQrlsRYs6t3mSfnZm/YPUjNP4c
# p09gV0QJINME5PmI1yyJnRDtx2Kxo5R5XpGo2npBAzhGes9oWC8kBf71fu8JVlC1
# BT6JZUTPgB4TT1pdHCVgpxuQrUZuF7+mN3nw8bir0zbUI3IPRjlkJT/wFcSL/5Cx
# kdMmmgihQdvdxrv8ZB0hsz3169DWEO6Kbp0XNyjLH2JzKR/djI4Ax1myfcRDDUJA
# xxthipA/REYK146rCWgwMflgCWGSwvTl8rhI3wIDAQABo4IBcTCCAW0wEwYDVR0l
# BAwwCgYIKwYBBQUHAwMwHQYDVR0OBBYEFKeldy5Qis7WN8kI9MHPQK2k2CvHMFEG
# A1UdEQRKMEikRjBEMQ0wCwYDVQQLEwRNT1BSMTMwMQYDVQQFEyozMTYzNSsyMDEy
# MzE2ZC0wZWVhLTQyNTMtYmViZi0xYTA4NDk0YmFjZDkwHwYDVR0jBBgwFoAUdOZv
# RTZymrmwNMeHBS/V62EnHCIwUwYDVR0fBEwwSjBIoEagRIZCaHR0cDovL3d3dy5t
# aWNyb3NvZnQuY29tL3BraW9wcy9jcmwvTWljTWFyUHJvQ0EyMDExXzIwMTEtMTEt
# MTQuY3JsMGAGCCsGAQUFBwEBBFQwUjBQBggrBgEFBQcwAoZEaHR0cDovL3d3dy5t
# aWNyb3NvZnQuY29tL3BraW9wcy9jZXJ0cy9NaWNNYXJQcm9DQTIwMTFfMjAxMS0x
# MS0xNC5jcnQwDAYDVR0TAQH/BAIwADANBgkqhkiG9w0BAQsFAAOCAQEAZEMdBT6Z
# SM5c8I+HZsf0g+AGVgvTGYtv/gCuh0I/rpnROX9k4YsN1MepAkJn0iQ8kTEW70eh
# ccbYS5fcp64IF2ODtFfr7YWHusLQgE7TB5w1OxZZlYA1Bg6pIDDdEO20dXWg6btL
# 9ATv65fZr0y6GmWa9034w/MOqvGeYa4HMgacxjEzxBMHD7MM3VeAk/ZPrIGG3iuv
# pVVFmbIB5yO9IHQt0yT7iC9qFcN0xNGcr098yk4DURIoo1NAXHbe6m9fhfyoRRhc
# M/TUlQHzEuur89fu6Eu+2Lf+iZiELaAwrxaaB5OzYH8fly3U70iQ6ySNfuCyS3hL
# rNuryQVxKF4aEjCCBdAwggO4oAMCAQICCmEJy3IAAAAAACYwDQYJKoZIhvcNAQEL
# BQAwfTELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcT
# B1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEnMCUGA1UE
# AxMeTWljcm9zb2Z0IE1hcmtldFBsYWNlIFBDQSAyMDExMB4XDTExMTExNDIyNTE0
# NVoXDTIxMTExNDIzMDE0NVowgYcxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNo
# aW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29y
# cG9yYXRpb24xMTAvBgNVBAMTKE1pY3Jvc29mdCBNYXJrZXRwbGFjZSBQcm9kdWN0
# aW9uIENBIDIwMTEwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCXJRuZ
# JJh5u/KcsFUa/GREaBXUblIKCYWXntZTsI5/pdxH9XRC8NTT/Ixt5YleUjGu3VCn
# CMyI7kABm2Uh4/N0Gewq4Tgemam6lrQ/OKoJQNtJPbZ5aJqCJkLbTy5rjcY4ZhAt
# IUYlRtEHTvlqnGQqu6WslPll7tHUvYwxWzrwO0tVSnEhYdzbaIqOBEqycMEWdyhP
# eCwvkExvMeEYDtXzdVK3f4LRSIIBCS+ZBEVaeGhSbYRvBpwNGslKi0KEAfI25rcA
# kuHXX5RlYWDWPUdOiWyNq3HBPHO+RFF+c0zL+awi7OdBJMQI5iaqJTmt3vZPlUhL
# yVcPGwKkaT57prV1AgMBAAGjggFFMIIBQTAQBgkrBgEEAYI3FQEEAwIBADAdBgNV
# HQ4EFgQUdOZvRTZymrmwNMeHBS/V62EnHCIwGQYJKwYBBAGCNxQCBAweCgBTAHUA
# YgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB/wQFMAMBAf8wHwYDVR0jBBgwFoAU
# D1PLPxZhJf5giR3TuXzokK2zlNEwVwYDVR0fBFAwTjBMoEqgSIZGaHR0cDovL2Ny
# bC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJvZHVjdHMvTWljTWFyUENBMjAxMV8y
# MDExLTAzLTI4LmNybDBbBggrBgEFBQcBAQRPME0wSwYIKwYBBQUHMAKGP2h0dHA6
# Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljTWFyUENBMjAxMV8yMDEx
# LTAzLTI4LmNydDANBgkqhkiG9w0BAQsFAAOCAgEAb1IIVH9V22nnvI9+9N4nLpru
# iW3UGA/s4gYt4yCaCTepvAb/MPxoxsXRtmo6WZPL40ICXDTs87/lHsUFM+XfbnVK
# r9P16AQeivvh/xzqKiOKxgB+WtKtPOdUFB4nRkd3CjrmdtK83YgLU0fxZ30wn1ps
# YicClvf/Vxrvlex7v8Hog1gD0aBzQrH+aG+KmFOP0Ty5NAX1btGrhTrfufduYhBr
# CiYBZ2LRItAar0HOmMswoAZhj3LW32ewdWAnJPDtzV5r/EXVY0+PyLdFBf7DxAqm
# O0+yPIy4S+X7H+ARbQpXZ85uNQkcR7K8c1UyNTbzN0dn8M7h2+oCa0BbvanwNJ/D
# YSc519ZqUJrrwF/ABUALqoj+FLNPv/dAM04zajn4/O6qac+JdstRcg5u63hphCDZ
# e2jtEAkUiBAztFzhg7Zv0nLkp2psp3YZdf5R8ESN2zxWa6JANZuRTOTK/5rMDI+S
# MARZuBDBX5FAYTnhbK3RzNJTQeYhHOit2A4vxUY8YYmI4BsPxNQQOAW6+3y5qcoX
# vOMIKAb24cRq3Yr11M5WU0oRO/1zcpQVOViZ5k8DeeA6bZ2G4PdDUtQ0CF1DAZbZ
# izxbNgdOZO4oBPVm2kv+K/QsFNEuL0qIeu7goOJHKdSKioWjITADYQQjzX6O4lhI
# ZKG9Rs52TcPG/Z1V1K4wggbXMIIEv6ADAgECAgphEkSiAAAAAAACMA0GCSqGSIb3
# DQEBCwUAMIGIMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4G
# A1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMTIw
# MAYDVQQDEylNaWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkgMjAx
# MTAeFw0xMTAzMjgyMTA5MzlaFw0zMTAzMjgyMTE5MzlaMH0xCzAJBgNVBAYTAlVT
# MRMwEQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQK
# ExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xJzAlBgNVBAMTHk1pY3Jvc29mdCBNYXJr
# ZXRQbGFjZSBQQ0EgMjAxMTCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIB
# ALm1GksBmFbBNzAJz375qM1IQAd6sQQAYCUVtbw6ePrVYp18Q99n+5NSvPSUOswz
# oEv4g4FCwvshStSv9UI2nP9DOz+nPL+S0o51hpRES2v6mN9o+Oku0i0HIrd4No4K
# E0ztN3lBvvch2VExlfxSXUWAfLl4a85yQWvmPshsIkChfpulxXgR/yjkaChcfnZj
# KMvRFif5v6PSIlI9pM5vQuvdfr2fyNsNAY/d1vUjz5FdjwjR5c4HyAKHmo1qR5ot
# /Fot8WR6J5MwFQR8T7AgaaTqDbFr5gH1DO3gbYZEj7xa7npMgwjzRGIXxqoKrai5
# KAXm1y2Qjlcmxe8E0Gi77HutP/9z9NN4nEXAqEGIGODQKqxe4/NKOVOw5Hl8XAdM
# b3p4LGioIgGldQypEW6XvfMAqq9QAEr5uawCVGDFPpl5jonldhHpWNoxq3p1nvRU
# KTV2FdTaFgYdFiqBcbbaFmAJxe9OWyFrUmbM9PGXyuli1+BlzS3DwZYAs305tbHS
# mAElnQWx9oWmjYT2qRYUEASRysJ1VtolxKyJobj4uOVtzFU2eQ/QErIWcevXC2GO
# sQHSGvvPSn/7ka7BjlWX2oQ7g8PjSNfN4gMswvM/0Ebdj99i2MT9MawnalYfgyaN
# ouDaBPFJNbrAnW1UbgTAUJfwMKTk6uyDLxxhJX+uQqs1AgMBAAGjggFLMIIBRzAQ
# BgkrBgEEAYI3FQEEAwIBADAdBgNVHQ4EFgQUD1PLPxZhJf5giR3TuXzokK2zlNEw
# GQYJKwYBBAGCNxQCBAweCgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8GA1UdEwEB
# /wQFMAMBAf8wHwYDVR0jBBgwFoAUci06AjGQQ7kUBU7h6qfHMdEjiTQwWgYDVR0f
# BFMwUTBPoE2gS4ZJaHR0cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9jcmwvcHJv
# ZHVjdHMvTWljUm9vQ2VyQXV0MjAxMV8yMDExXzAzXzIyLmNybDBeBggrBgEFBQcB
# AQRSMFAwTgYIKwYBBQUHMAKGQmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kv
# Y2VydHMvTWljUm9vQ2VyQXV0MjAxMV8yMDExXzAzXzIyLmNydDANBgkqhkiG9w0B
# AQsFAAOCAgEAo7mZjPGVTQ4KfcB7C+EWPCifJy4p78RU6YTRgq8Wi5J5GBuaZjIV
# ZV/lAf7UolvFtd1utJ4IM/buccDMe7/9bf04JuoNm61cjiiOQO7uv9kiCcJCShcP
# edytGCClU9vMPc5fHUiZ5kZ8kdBvOp3Fmxegi0OgOpQUPaNpkxlag6+Jt+VigJ3O
# mMVFDNcu+Jm8xRWa+t1R7AnDqd6V1p1KgGNa2+kOjV9W93JZ3CqixZtBIyh3EOeo
# K028Lrge6THYfyBQiUdNTEM0Gw7aRIrGznmVIuN2FQAByqiNRDvW0cIN36wo8AG5
# pfTJiOLrTFveIFhYL6zMSF/p+NkQQyI39zrd45E+oLd13k05jvW6bYG5WYpoREXZ
# BUVBaVbZKHCXi6Brrsz/d7h/2KdN9sM0R/l5jILo32b/r535ZSasXaMKEC8eeRcS
# y3k3mH8Iv8TEo2TtoDb3p91P9ynkgndZFDhUtwDq8GdFDTlyXluSUAulX3dQWrxW
# ehQLWtXzC7t1hbzQfhanh1GlN8mu65rcSZ85NvQ3kql4BjZlUssAUuDWoyOYySCg
# AOlITSZbIPAh2MFDin+Xo/DqEApG2nVMV8oAH8Qn1tmAW7o7SPj6Mno0RcVlI2BV
# juS3kyOtlDMqnzhIqroKGIzcZkGZWtrOGB1bqiAt+EfORUJYPaSkAbAxghXEMIIV
# wAIBATCBnzCBhzELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAO
# BgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEx
# MC8GA1UEAxMoTWljcm9zb2Z0IE1hcmtldHBsYWNlIFByb2R1Y3Rpb24gQ0EgMjAx
# MQITMwAAABcDq8CQnk/7/AAAAAAAFzANBglghkgBZQMEAgEFAKCBxDAZBgkqhkiG
# 9w0BCQMxDAYKKwYBBAGCNwIBBDAcBgorBgEEAYI3AgELMQ4wDAYKKwYBBAGCNwIB
# FTAvBgkqhkiG9w0BCQQxIgQguNoQhNm+aVUXWiaSZ0Y1UHF5oWnqG8EPRwvrw2Hk
# V4YwWAYKKwYBBAGCNwIBDDFKMEigJoAkAEsAaQBuAGUAYwB0ACAAZgBvAHIAIABX
# AGkAbgBkAG8AdwBzoR6AHGh0dHA6Ly9raW5lY3Rmb3J3aW5kb3dzLmNvbSAwDQYJ
# KoZIhvcNAQEBBQAEggEAEkaVCS1ykfoMb92tsTCJub/8jfUAE4eUCWdKzbun5o+P
# 7v7hou8/EOjrpFV7BR+sfkah8b2rHLBPdeqs61q/FOPwhslgr/j+Aa4R9mgdY/ER
# SBu5WCcRC87Wv3MBZ6wRvAhiNOfvEF9A2j74QQ13Lcz8UNflx9erLG7TwA9R+l6C
# IN+gwFIzoqckOrhUDyWDpEsqxD/SVvs5e9daHwFnPwIk4eQ6sOsopZCRZ2bOqJYc
# gyoSrrMSim45j4B9EqxH9aY/msjUhkVpYyFfxb673t9936l0EHyHcqhd8xEcF4VT
# DJLxgGkjFQR72AA9Yi91JrmBP79A3Mo7g3kwgnSrE6GCEy4wghMqBgorBgEEAYI3
# AwMBMYITGjCCExYGCSqGSIb3DQEHAqCCEwcwghMDAgEDMQ8wDQYJYIZIAWUDBAIB
# BQAwggExBgsqhkiG9w0BCRABBKCCASAEggEcMIIBGAIBAQYKKwYBBAGEWQoDATAx
# MA0GCWCGSAFlAwQCAQUABCAYxYQ0qn0Q8Kp+/ysLq7OqrFyjXB/e9YpLbhlSiLIf
# eQIGVBrtCMFgGA8yMDE0MTAxOTIwMzI0N1owBwIBAYACAfSggbGkga4wgasxCzAJ
# BgNVBAYTAlVTMQswCQYDVQQIEwJXQTEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UE
# ChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMQ0wCwYDVQQLEwRNT1BSMScwJQYDVQQL
# Ex5uQ2lwaGVyIERTRSBFU046MzFDNS0zMEJBLTdDOTExJTAjBgNVBAMTHE1pY3Jv
# c29mdCBUaW1lLVN0YW1wIFNlcnZpY2Wggg69MIIGcTCCBFmgAwIBAgIKYQmBKgAA
# AAAAAjANBgkqhkiG9w0BAQsFADCBiDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldh
# c2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBD
# b3Jwb3JhdGlvbjEyMDAGA1UEAxMpTWljcm9zb2Z0IFJvb3QgQ2VydGlmaWNhdGUg
# QXV0aG9yaXR5IDIwMTAwHhcNMTAwNzAxMjEzNjU1WhcNMjUwNzAxMjE0NjU1WjB8
# MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVk
# bW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSYwJAYDVQQDEx1N
# aWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAxMDCCASIwDQYJKoZIhvcNAQEBBQAD
# ggEPADCCAQoCggEBAKkdDbx3EYo6IOz8E5f1+n9plGt0VBDVpQoAgoX77XxoSyxf
# xcPlYcJ2tz5mK1vwFVMnBDEfQRsalR3OCROOfGEwWbEwRA/xYIiEVEMM1024OAiz
# Qt2TrNZzMFcmgqNFDdDq9UeBzb8kYDJYYEbyWEeGMoQedGFnkV+BVLHPk0ySwcSm
# XdFhE24oxhr5hoC732H8RsEnHSRnEnIaIYqvS2SJUGKxXf13Hz3wV3WsvYpCTUBR
# 0Q+cBj5nf/VmwAOWRH7v0Ev9buWayrGo8noqCjHw2k4GkbaICDXoeByw6ZnNPOcv
# RLqn9NxkvaQBwSAJk3jN/LzAyURdXhacAQVPIk0CAwEAAaOCAeYwggHiMBAGCSsG
# AQQBgjcVAQQDAgEAMB0GA1UdDgQWBBTVYzpcijGQ80N7fEYbxTNoWoVtVTAZBgkr
# BgEEAYI3FAIEDB4KAFMAdQBiAEMAQTALBgNVHQ8EBAMCAYYwDwYDVR0TAQH/BAUw
# AwEB/zAfBgNVHSMEGDAWgBTV9lbLj+iiXGJo0T2UkFvXzpoYxDBWBgNVHR8ETzBN
# MEugSaBHhkVodHRwOi8vY3JsLm1pY3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0
# cy9NaWNSb29DZXJBdXRfMjAxMC0wNi0yMy5jcmwwWgYIKwYBBQUHAQEETjBMMEoG
# CCsGAQUFBzAChj5odHRwOi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01p
# Y1Jvb0NlckF1dF8yMDEwLTA2LTIzLmNydDCBoAYDVR0gAQH/BIGVMIGSMIGPBgkr
# BgEEAYI3LgMwgYEwPQYIKwYBBQUHAgEWMWh0dHA6Ly93d3cubWljcm9zb2Z0LmNv
# bS9QS0kvZG9jcy9DUFMvZGVmYXVsdC5odG0wQAYIKwYBBQUHAgIwNB4yIB0ATABl
# AGcAYQBsAF8AUABvAGwAaQBjAHkAXwBTAHQAYQB0AGUAbQBlAG4AdAAuIB0wDQYJ
# KoZIhvcNAQELBQADggIBAAfmiFEN4sbgmD+BcQM9naOhIW+z66bM9TG+zwXiqf76
# V20ZMLPCxWbJat/15/B4vceoniXj+bzta1RXCCtRgkQS+7lTjMz0YBKKdsxAQEGb
# 3FwX/1z5Xhc1mCRWS3TvQhDIr79/xn/yN31aPxzymXlKkVIArzgPF/UveYFl2am1
# a+THzvbKegBvSzBEJCI8z+0DpZaPWSm8tv0E4XCfMkon/VWvL/625Y4zu2JfmttX
# QOnxzplmkIz/amJ/3cVKC5Em4jnsGUpxY517IW3DnKOiPPp/fZZqkHimbdLhnPkd
# /DjYlPTGpQqWhqS9nhquBEKDuLWAmyI4ILUl5WTs9/S/fmNZJQ96LjlXdqJxqgaK
# D4kWumGnEcua2A5HmoDF0M2n0O99g/DhO3EJ3110mCIIYdqwUB5vvfHhAN/nMQek
# kzr3ZUd46PioSKv33nJ+YWtvd6mBy6cJrDm77MbL2IK0cs0d9LiFAR6A+xuJKlQ5
# slvayA1VmXqHczsI5pgt6o3gMy4SKfXAL1QnIffIrE7aKLixqduWsqdCosnPGUFN
# 4Ib5KpqjEWYw07t0MkvfY3v1mYovG8chr1m1rtxEPJdQcdeh0sVV42neV8HR3jDA
# /czmTfsNv11P6Z0eGTgvvM9YBS7vDaBQNdrvCScc1bN+NR4Iuto229Nfj950iEkS
# MIIE0jCCA7qgAwIBAgITMwAAAE+t6FSVUCiUZwAAAAAATzANBgkqhkiG9w0BAQsF
# ADB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMH
# UmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSYwJAYDVQQD
# Ex1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAxMDAeFw0xNDA1MjMxNzIwMDha
# Fw0xNTA4MjMxNzIwMDhaMIGrMQswCQYDVQQGEwJVUzELMAkGA1UECBMCV0ExEDAO
# BgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEN
# MAsGA1UECxMETU9QUjEnMCUGA1UECxMebkNpcGhlciBEU0UgRVNOOjMxQzUtMzBC
# QS03QzkxMSUwIwYDVQQDExxNaWNyb3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNlMIIB
# IjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoVWSd5DCLVEuFWGAmgxVljok
# hTeOKBqyNu5kKVyJDM/R3TP0S/ybFLZPb5919WRZlYpBd5iYPmZ6OLE0+EWmhBHT
# NaGl81B7hcDvHiXxrjz4/jze7PfuwzIGVi1hpASd8E+YKob9Zuv1x0f1BXH3gXg0
# pVkWdJr94SreENEt+qqIOaRFM3i37Dx3djHjUJXPHpMGCHpsTaSbnyblhL/7njEu
# KuCbjiceWZAK63nwgZNnoICuD/WDVyoasVsxRkL+aFKAIDi4U1JFy09znrNJMFuw
# zm8/o+o5bonxVYkgLFETIGqDaBVCnsmFmZzOj4O0b5bdDKM87xOHDH6ajeIWeQID
# AQABo4IBGzCCARcwHQYDVR0OBBYEFCul6RBT0BLzGRtALApNF8zERckuMB8GA1Ud
# IwQYMBaAFNVjOlyKMZDzQ3t8RhvFM2hahW1VMFYGA1UdHwRPME0wS6BJoEeGRWh0
# dHA6Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY1RpbVN0
# YVBDQV8yMDEwLTA3LTAxLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKG
# Pmh0dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljVGltU3RhUENB
# XzIwMTAtMDctMDEuY3J0MAwGA1UdEwEB/wQCMAAwEwYDVR0lBAwwCgYIKwYBBQUH
# AwgwDQYJKoZIhvcNAQELBQADggEBAGFuC/kWMPinON9WEaF+TbcaibwiniR9FOhu
# qLu6nUkcFNaJ2JRUrCizNT1M+L49O4I0EMmBa4whc0ZyldFMtMQ0Nb0xqWfrKnvM
# VQS1ga6VZIVP0QYwt+MFMugGXgwbFTwgO+KIzoa4LLu9XRBuw194FmmjvrWPjawc
# lxy7ss9OTxUPbhbV2IgRoKN48lYVEEqJ1NwBBEeSLTEMrjFhs30YWdx+FdoyVCfh
# M3+ZHHpgFMQuOI/VEfet1b0j81fSRd8Ia2H8FtVy2nqsZGQrw1BoeSr2teq34Lvm
# xXABLH8nEyRdLWr5GfBuKWyj1GouyIT1KzNCM0rNz6t5uEelQQuhggNuMIICVgIB
# ATCB26GBsaSBrjCBqzELMAkGA1UEBhMCVVMxCzAJBgNVBAgTAldBMRAwDgYDVQQH
# EwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRpb24xDTALBgNV
# BAsTBE1PUFIxJzAlBgNVBAsTHm5DaXBoZXIgRFNFIEVTTjozMUM1LTMwQkEtN0M5
# MTElMCMGA1UEAxMcTWljcm9zb2Z0IFRpbWUtU3RhbXAgU2VydmljZaIlCgEBMAkG
# BSsOAwIaBQADFQAovJHaQX7mhtCTXJWEeLe7nFZZqqCBwjCBv6SBvDCBuTELMAkG
# A1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQx
# HjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjENMAsGA1UECxMETU9QUjEn
# MCUGA1UECxMebkNpcGhlciBOVFMgRVNOOjU3RjYtQzFFMC01NTRDMSswKQYDVQQD
# EyJNaWNyb3NvZnQgVGltZSBTb3VyY2UgTWFzdGVyIENsb2NrMA0GCSqGSIb3DQEB
# BQUAAgUA1+4sHTAiGA8yMDE0MTAxOTEyMjUwMVoYDzIwMTQxMDIwMTIyNTAxWjB0
# MDoGCisGAQQBhFkKBAExLDAqMAoCBQDX7iwdAgEAMAcCAQACAgnyMAcCAQACAhen
# MAoCBQDX732dAgEAMDYGCisGAQQBhFkKBAIxKDAmMAwGCisGAQQBhFkKAwGgCjAI
# AgEAAgMW42ChCjAIAgEAAgMHoSAwDQYJKoZIhvcNAQEFBQADggEBAJrSFu1hh6fk
# uySFYzYugo8htOhDw1ZrPUKMJjqfH4K5/InZ+IBy8PAM5sIMwE/k/FPscEfYWyF2
# KjJgE3cHylr6ji+gaIkhV46Hnp7yDnT8KX0KdtvxxqIvdcL6Wd3gLHGumOmqj8gW
# ywgrHo2w9Tb9xYjtrsXpGPPJNKUKiAzBoL4TMxaNdIUndRET1lgD2bm0S35DMIgW
# GBGudwmkynd4sdBY4Lgf3Z0Wov2hwAR+qjiZkckIJk6j/exGiUAwLMasWjJl2EdQ
# WeS259SIb905QY7MbffzLAsO12BfVxuDZaHwXw0E3qnolsG16VDkhyjpplRYakhw
# lyCHyNPwNiAxggL1MIIC8QIBATCBkzB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMK
# V2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0
# IENvcnBvcmF0aW9uMSYwJAYDVQQDEx1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0Eg
# MjAxMAITMwAAAE+t6FSVUCiUZwAAAAAATzANBglghkgBZQMEAgEFAKCCATIwGgYJ
# KoZIhvcNAQkDMQ0GCyqGSIb3DQEJEAEEMC8GCSqGSIb3DQEJBDEiBCBgVKNr0QN0
# 5Is/xJs4BFeuD2Q6USRgpx0Cuk+bsag5gzCB4gYLKoZIhvcNAQkQAgwxgdIwgc8w
# gcwwgbEEFCi8kdpBfuaG0JNclYR4t7ucVlmqMIGYMIGApH4wfDELMAkGA1UEBhMC
# VVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNV
# BAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9zb2Z0IFRp
# bWUtU3RhbXAgUENBIDIwMTACEzMAAABPrehUlVAolGcAAAAAAE8wFgQUlKXZva2g
# Jdc7BWw2Ery83pQjbHQwDQYJKoZIhvcNAQELBQAEggEAiXAakZVgvXswU8DHasHc
# rLw0krSV3ukzlgXGuOnwXc83msivlU9l9OpZ0ladQCMHC0vY4o5Nec2GlY5EGnk6
# R8bEQ6TzW9IadnYjOPOKMdV3KViuVLGUcQfxyScMR4b6/RKmJbOUu5MzAQC/qPMi
# c0Cwbbk6H96Vq5C9MO1GWEoKZOX3ThIj7SAUNVz3C7nmdbKpA69FEdH5DsEcSzOW
# EkTJZZep0ZgX7rNftT8cE8AJugKegoh+rWMnlk1c33Pv+svPyuoBBmoeQUCHved6
# qPWRSFZ9cyWHlK4d5lHY5KhSr/5bPlI7VaaTzwkJKjRiBBCN4iak4q+DkJcwJ8Su
# RA==
# SIG # End signature block
