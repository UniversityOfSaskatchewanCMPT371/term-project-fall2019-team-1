@Library('shared-library')_

def paramMap = [branch:'*/develop', jobName:'Unity Player Builder']
buildNotifyPipe paramMap
