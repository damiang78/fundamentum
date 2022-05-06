Feature: Weather

Get the weather forecast

Scenario: Returns the weather forecast
	When requesting the weather forecast
	Then we should get the weather forecast
