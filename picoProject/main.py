import network

ssid = 'TP-Link_16B9'
password = '45242625'

wlan = network.WLAN()
wlan.active(True)
wlan.connect(ssid,password)